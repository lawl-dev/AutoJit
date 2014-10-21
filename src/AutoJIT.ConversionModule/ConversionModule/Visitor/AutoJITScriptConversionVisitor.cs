using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Visitor
{
    public class AutoJITScriptConversionVisitor : FunctionVisitor, IFunctionSyntaxVisitor<AutoitScriptRootNode, NamespaceDeclarationSyntax>
    {
        private readonly ICSharpSkeletonFactory _sharpSkeletonFactory;

        public AutoJITScriptConversionVisitor( IInjectionService injectionService, ICSharpSkeletonFactory sharpSkeletonFactory, IContextService contextService )
            : base( injectionService, contextService ) {
            _sharpSkeletonFactory = sharpSkeletonFactory;
        }

        public NamespaceDeclarationSyntax Visit( AutoitScriptRootNode @in ) {
            var memberList = new SyntaxList<MemberDeclarationSyntax>();

            ContextService.SetGlobalContext( true );
            memberList = memberList.Add( @in.MainFunctionNode.Accept( this ) );
            ContextService.SetGlobalContext( false );
            memberList = memberList.AddRange( ContextService.PopGlobalVariables() );
            ContextService.ResetFunctionContext();

            foreach (FunctionNode function in @in.Functions) {
                memberList = memberList.Add( function.Accept( this ) );
                memberList = memberList.AddRange( ContextService.PopGlobalVariables() );
                ContextService.ResetFunctionContext();
            }

            NamespaceDeclarationSyntax finalScript = _sharpSkeletonFactory.EmbedInClassTemplate(
                new List<MemberDeclarationSyntax>( memberList ), ContextService.GetRuntimeInstanceName(), "AutoJITScriptClass",
                ContextService.GetContextInstanceName() );

            finalScript = RemoveEmptyStatements( finalScript );

            finalScript = FixByReferenceCalls( finalScript, memberList );

            return finalScript;
        }

        private static NamespaceDeclarationSyntax RemoveEmptyStatements( NamespaceDeclarationSyntax finalScript ) {
            List<EmptyStatementSyntax> emptyStatements =
                finalScript.DescendantNodes().OfType<EmptyStatementSyntax>().Where( x => x.Parent.GetType() != typeof (LabeledStatementSyntax) ).ToList();
            finalScript = finalScript.RemoveNodes( emptyStatements, SyntaxRemoveOptions.KeepEndOfLine );
            return finalScript;
        }

        private static NamespaceDeclarationSyntax FixByReferenceCalls( NamespaceDeclarationSyntax finalScript, SyntaxList<MemberDeclarationSyntax> memberList ) {
            var toReplace = new Dictionary<ArgumentSyntax, ArgumentSyntax>();
            IEnumerable<ArgumentSyntax> argumentSyntaxs = finalScript.DescendantNodes().OfType<ArgumentSyntax>();
            foreach (ArgumentSyntax argumentSyntax in argumentSyntaxs) {
                var invocationExpressionSyntax = argumentSyntax.Parent.Parent as InvocationExpressionSyntax;
                if ( invocationExpressionSyntax != null &&
                     invocationExpressionSyntax.Expression is IdentifierNameSyntax ) {
                    string functionName = ( (IdentifierNameSyntax) invocationExpressionSyntax.Expression ).Identifier.Text;
                    MethodDeclarationSyntax methodDeclarationSyntax =
                        memberList.OfType<MethodDeclarationSyntax>().SingleOrDefault( x => x.Identifier.Text == functionName );

                    if ( methodDeclarationSyntax != null ) {
                        List<ParameterSyntax> parameterSyntaxs =
                            methodDeclarationSyntax.ParameterList.Parameters.Where( x => x.Modifiers.Any( m => m.ValueText.Equals( "ref" ) ) ).ToList();
                        var argumentListSyntax = argumentSyntax.Parent as ArgumentListSyntax;
                        int indexOfArgument = argumentListSyntax.Arguments.IndexOf( argumentSyntax );

                        foreach (ParameterSyntax parameterSyntax in parameterSyntaxs) {
                            int indexOfRefParameter = methodDeclarationSyntax.ParameterList.Parameters.IndexOf( parameterSyntax );

                            if ( indexOfArgument == indexOfRefParameter ) {
                                ArgumentSyntax withRef = argumentSyntax.WithRefOrOutKeyword( SyntaxFactory.Token( SyntaxKind.RefKeyword ) );
                                toReplace.Add( argumentSyntax, withRef );
                            }
                        }
                    }
                }
            }

            finalScript = finalScript.ReplaceNodes( toReplace.Keys, ( old, @new ) => toReplace[old] );
            return finalScript;
        }
    }
}
