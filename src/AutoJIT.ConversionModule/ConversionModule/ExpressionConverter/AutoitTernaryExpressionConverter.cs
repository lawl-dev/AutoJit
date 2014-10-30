using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitTernaryExpressionConverter : AutoitExpressionConverterBase<TernaryExpression>
    {
        public AutoitTernaryExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( TernaryExpression node, IContextService contextService ) {
            ExpressionSyntax conditionExpression = ConverGeneric( node.Condition, contextService );
            ExpressionSyntax ifTrueExpression = ConverGeneric( node.IfTrue, contextService );
            ExpressionSyntax ifFalseExpression = ConverGeneric( node.IfFalse, contextService );

            return SyntaxFactory.ConditionalExpression( conditionExpression, ifTrueExpression, ifFalseExpression );
        }
    }
}
