using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitTernaryExpressionConverter : AutoitExpressionConverterBase<TernaryExpression>
    {
        public AutoitTernaryExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( TernaryExpression node, IContextService contextService ) {
            ExpressionSyntax conditionExpression = ConvertGeneric( node.Condition, contextService );
            ExpressionSyntax ifTrueExpression = ConvertGeneric( node.IfTrue, contextService );
            ExpressionSyntax ifFalseExpression = ConvertGeneric( node.IfFalse, contextService );

            return SyntaxFactory.ConditionalExpression( conditionExpression, ifTrueExpression, ifFalseExpression );
        }
    }
}
