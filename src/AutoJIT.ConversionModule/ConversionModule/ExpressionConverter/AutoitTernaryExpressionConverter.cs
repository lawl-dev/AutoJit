using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitTernaryExpressionConverter : AutoitExpressionConverterBase<TernaryExpression>
    {
        public AutoitTernaryExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( TernaryExpression node, IContextService context ) {
            return SyntaxFactory.ConditionalExpression(
                ConverGeneric( node.Condition, context ),
                ConverGeneric( node.IfTrue, context ),
                ConverGeneric( node.IfFalse, context ) );
        }
    }
}
