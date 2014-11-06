using System.Collections.Generic;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitCallExpressionConverter : AutoitInvocationExpressionConverterBase<CallExpression>
    {
        public AutoitCallExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( CallExpression node, IContextService contextService ) {
            string runtimeInstanceName = contextService.GetRuntimeInstanceName();
            string functionName = node.IdentifierName;
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.Parameter, contextService );

            return CreateInvocationExpression( runtimeInstanceName, functionName, parameter );
        }
    }
}
