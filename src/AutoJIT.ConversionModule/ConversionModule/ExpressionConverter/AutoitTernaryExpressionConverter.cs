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
            var conditionExpression = ConverGeneric( node.Condition, context );
            var ifTrueExpression = ConverGeneric( node.IfTrue, context );
            var ifFalseExpression = ConverGeneric( node.IfFalse, context );

            return SyntaxFactory.ConditionalExpression(
                conditionExpression,
                ifTrueExpression,
                ifFalseExpression );
        }
    }
}
