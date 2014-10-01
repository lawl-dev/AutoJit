using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBooleanNegateExpressionConverter : AutoitInvocationExpressionConverterBase<BooleanNegateExpression>
    {
        public AutoitBooleanNegateExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert(BooleanNegateExpression node, IContextService context)
        {
            return CreateInvocationExpression(
                context.GetRuntimeInstanceName(),
                node.NOTCompilerFunctionName,
                CreateParameter( node.Left.ToEnumerable(), context ) );
        }
    }
}
