using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBooleanNegateExpressionConverter : AutoitInvocationExpressionConverterBase<BooleanNegateExpression>
    {
        public AutoitBooleanNegateExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( BooleanNegateExpression node, IContextService context ) {
            string runtimeInstanceName = context.GetRuntimeInstanceName();
            string compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.NOT( null ) );
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.Left.ToEnumerable(), context );

            return CreateInvocationExpression( runtimeInstanceName, compilerFunctionName, parameter );
        }
    }
}
