using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitArrayExpressionConverter : AutoitExpressionConverterBase<ArrayExpression>
    {
        public AutoitArrayExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( ArrayExpression node, IContextService context ) {
            IdentifierNameSyntax variable = SyntaxFactory.IdentifierName( node.IdentifierName );
            return SyntaxFactory.ElementAccessExpression(
                variable,
                SyntaxFactory.BracketedArgumentList(
                    SyntaxFactory.SeparatedList<ArgumentSyntax>()
                        .AddRange(
                            node.AccessParameter.Select(
                                x => SyntaxFactory.Argument( ConverGeneric( x, context ) ) ) ) ) );
        }
    }
}
