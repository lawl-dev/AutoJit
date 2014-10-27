using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitCallExpressionConverter : AutoitInvocationExpressionConverterBase<CallExpression>
    {
        public AutoitCallExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( CallExpression node, IContextService context ) {
            string runtimeInstanceName = context.GetRuntimeInstanceName();
            string functionName = node.IdentifierName;
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.Parameter, context );

            return CreateInvocationExpression( runtimeInstanceName, functionName, parameter );
        }
    }
}
