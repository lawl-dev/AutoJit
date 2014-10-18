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

            foreach (var function in @in.Functions) {
                memberList = memberList.Add( function.Accept( this ) );
                memberList = memberList.AddRange( ContextService.PopGlobalVariables() );
                ContextService.ResetFunctionContext();
            }

            var finalScript = _sharpSkeletonFactory.EmbedInClassTemplate(
                new List<MemberDeclarationSyntax>( memberList ), ContextService.GetRuntimeInstanceName(), "AutoJITScriptClass",
                ContextService.GetContextInstanceName() );

            finalScript = RemoveEmptyStatements( finalScript );

            finalScript = FixByReferenceCalls( finalScript, memberList );

            return finalScript;
        }

        private static NamespaceDeclarationSyntax RemoveEmptyStatements( NamespaceDeclarationSyntax finalScript ) {
            var emptyStatements =
                finalScript.DescendantNodes().OfType<EmptyStatementSyntax>().Where( x => x.Parent.GetType() != typeof (LabeledStatementSyntax) ).ToList();
            finalScript = finalScript.RemoveNodes( emptyStatements, SyntaxRemoveOptions.KeepEndOfLine );
            return finalScript;
        }

        private static NamespaceDeclarationSyntax FixByReferenceCalls( NamespaceDeclarationSyntax finalScript, SyntaxList<MemberDeclarationSyntax> memberList ) {
            var toReplace = new Dictionary<ArgumentSyntax, ArgumentSyntax>();
            var argumentSyntaxs = finalScript.DescendantNodes().OfType<ArgumentSyntax>();
            foreach (var argumentSyntax in argumentSyntaxs) {
                var invocationExpressionSyntax = argumentSyntax.Parent.Parent as InvocationExpressionSyntax;
                if ( invocationExpressionSyntax != null &&
                     invocationExpressionSyntax.Expression is IdentifierNameSyntax ) {
                    var functionName = ( (IdentifierNameSyntax) invocationExpressionSyntax.Expression ).Identifier.Text;
                    var methodDeclarationSyntax = memberList.OfType<MethodDeclarationSyntax>().SingleOrDefault( x => x.Identifier.Text == functionName );

                    if ( methodDeclarationSyntax != null ) {
                        var parameterSyntaxs =
                            methodDeclarationSyntax.ParameterList.Parameters.Where( x => x.Modifiers.Any( m => m.ValueText.Equals( "ref" ) ) ).ToList();
                        var argumentListSyntax = argumentSyntax.Parent as ArgumentListSyntax;
                        var indexOfArgument = argumentListSyntax.Arguments.IndexOf( argumentSyntax );

                        foreach (var parameterSyntax in parameterSyntaxs) {
                            var indexOfRefParameter = methodDeclarationSyntax.ParameterList.Parameters.IndexOf( parameterSyntax );

                            if ( indexOfArgument == indexOfRefParameter ) {
                                var withRef = argumentSyntax.WithRefOrOutKeyword( SyntaxFactory.Token( SyntaxKind.RefKeyword ) );
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
