using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Visitor
{
    public class FunctionVisitor : StatementConverterVisitor, IFunctionVisitor<MemberDeclarationSyntax>
    {
        public FunctionVisitor( IInjectionService injectionService, IContextService contextService ) : base( injectionService, contextService ) {}

        public MemberDeclarationSyntax Visit( FunctionNode @in ) {
            return Convert( @in, ContextService );
        }

        protected MemberDeclarationSyntax Convert( FunctionNode function, IContextService context ) {
            IList<IStatementNode> statementNodes = function.Statements;
            statementNodes = DeclareParameter( statementNodes, function.Parameter, context );

            List<StatementSyntax> dotNetStatements = ConvertStatements( statementNodes );

            dotNetStatements = OrderDeclarations( dotNetStatements );

            BlockSyntax body = dotNetStatements.ToBlock();

            return SyntaxFactory.MethodDeclaration( SyntaxFactory.IdentifierName( typeof (Variant).Name ), function.Name )
                .AddModifiers( SyntaxFactory.Token( SyntaxKind.PublicKeyword ) )
                .WithParameterList( SyntaxFactory.ParameterList( CreaterParameter( function.Parameter ).ToSeparatedSyntaxList() ) )
                .WithBody( body );
        }

        private IList<IStatementNode> DeclareParameter(
            IList<IStatementNode> statementNodes,
            IEnumerable<AutoitParameterInfo> parameter,
            IContextService context ) {
            foreach (AutoitParameterInfo parameterInfo in parameter) {
                context.Declare( parameterInfo.ParameterName );
                if ( parameterInfo.DefaultValue != null ) {
                    statementNodes.Insert( 0, new InitDefaultParameterStatement( parameterInfo.ParameterName, parameterInfo.DefaultValue ) );
                }
            }
            return statementNodes;
        }

        private static List<StatementSyntax> OrderDeclarations( List<StatementSyntax> cSharpStatements ) {
            List<LocalDeclarationStatementSyntax> allDeclarations =
                cSharpStatements.SelectMany( s => s.DescendantNodesAndSelf().OfType<LocalDeclarationStatementSyntax>() ).ToList();
            for ( int index = 0; index < cSharpStatements.Count; index++ ) {
                cSharpStatements[index] = cSharpStatements[index].ReplaceNodes( allDeclarations, ( node, syntaxNode ) => SyntaxFactory.EmptyStatement() );
            }
            cSharpStatements.InsertRange( 0, allDeclarations );
            return cSharpStatements;
        }

        private List<StatementSyntax> ConvertStatements( IEnumerable<IStatementNode> statements ) {
            return statements.SelectMany(
                x => x.Accpet( this ) ).ToList();
        }

        private IEnumerable<ParameterSyntax> CreaterParameter( IEnumerable<AutoitParameterInfo> parameters ) {
            return parameters.Select(
                p => {
                    ParameterSyntax parameter = SyntaxFactory.Parameter( SyntaxFactory.Identifier( p.ParameterName ) ).WithType(
                        SyntaxFactory.IdentifierName( typeof (Variant).Name )
                        );
                    if ( p.DefaultValue != null ) {
                        parameter = parameter.WithDefault(
                            SyntaxFactory.EqualsValueClause(
                                SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ) );
                    }

                    if ( p.IsByRef ) {
                        parameter = parameter.WithModifiers( new SyntaxTokenList().Add( SyntaxFactory.Token( SyntaxKind.RefKeyword ) ) );
                    }

                    return parameter;
                } );
        }
    }
}
