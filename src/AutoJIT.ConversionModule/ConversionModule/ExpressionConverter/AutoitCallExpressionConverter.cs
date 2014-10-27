using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitCallExpressionConverter : AutoitInvocationExpressionConverterBase<CallExpression>
    {
        public AutoitCallExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( CallExpression node, IContextService context ) {
            var runtimeInstanceName = context.GetRuntimeInstanceName();
            var functionName = node.IdentifierName;
            var parameter = CreateParameter( node.Parameter, context );
            
            return CreateInvocationExpression( runtimeInstanceName, functionName, parameter );
        }
    }
}
