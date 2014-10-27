using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitNegateExpressionConverter : AutoitInvocationExpressionConverterBase<NegateExpression>
    {
        public AutoitNegateExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( NegateExpression node, IContextService context ) {
            string runtimeInstanceName = context.GetRuntimeInstanceName();
            string negateFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Negate( null ) );
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.ExpressionNode.ToEnumerable(), context );

            return CreateInvocationExpression( runtimeInstanceName, negateFunctionName, parameter );
        }
    }
}
