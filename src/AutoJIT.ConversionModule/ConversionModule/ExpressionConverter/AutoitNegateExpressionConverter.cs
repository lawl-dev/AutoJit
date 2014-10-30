using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitNegateExpressionConverter : AutoitInvocationExpressionConverterBase<NegateExpression>
    {
        public AutoitNegateExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( NegateExpression node, IContextService contextService ) {
            string runtimeInstanceName = contextService.GetRuntimeInstanceName();
            string negateFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Negate( null ) );
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.ExpressionNode.ToEnumerable(), contextService );

            return CreateInvocationExpression( runtimeInstanceName, negateFunctionName, parameter );
        }
    }
}
