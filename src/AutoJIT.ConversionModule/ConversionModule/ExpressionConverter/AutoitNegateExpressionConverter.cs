using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitNegateExpressionConverter : AutoitInvocationExpressionConverterBase<NegateExpression>
    {
        public AutoitNegateExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( NegateExpression node, IContextService context ) {
            return CreateInvocationExpression(
                context.GetRuntimeInstanceName(),
                node.NegateFunctionName,
                CreateParameter( node.ExpressionNode.ToEnumerable(), context ) );
        }
    }
}
