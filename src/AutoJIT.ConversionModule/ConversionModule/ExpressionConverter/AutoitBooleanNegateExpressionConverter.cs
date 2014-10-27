using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBooleanNegateExpressionConverter : AutoitInvocationExpressionConverterBase<BooleanNegateExpression>
    {
        public AutoitBooleanNegateExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( BooleanNegateExpression node, IContextService context ) {
            var runtimeInstanceName = context.GetRuntimeInstanceName();
            var compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.NOT( null ) );
            var parameter = CreateParameter( node.Left.ToEnumerable(), context );
            
            return CreateInvocationExpression(runtimeInstanceName, compilerFunctionName, parameter );
        }
    }
}
