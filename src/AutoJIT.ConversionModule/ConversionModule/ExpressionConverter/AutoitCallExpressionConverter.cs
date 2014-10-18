using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitCallExpressionConverter : AutoitInvocationExpressionConverterBase<CallExpression>
    {
        public AutoitCallExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( CallExpression node, IContextService context ) {
            return CreateInvocationExpression( context.GetRuntimeInstanceName(), node.IdentifierName, CreateParameter( node.Parameter, context ) );
        }
    }
}
