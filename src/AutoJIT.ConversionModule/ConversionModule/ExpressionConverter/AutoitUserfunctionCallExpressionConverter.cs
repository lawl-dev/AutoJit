using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitUserfunctionCallExpressionConverter : AutoitInvocationExpressionConverterBase<UserfunctionCallExpression>
    {
        public AutoitUserfunctionCallExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert(UserfunctionCallExpression node, IContextService context)
        {
            return CreateInvocationExpression( node.IdentifierName, CreateParameter( node.Parameter, context ) );
        }
    }
}
