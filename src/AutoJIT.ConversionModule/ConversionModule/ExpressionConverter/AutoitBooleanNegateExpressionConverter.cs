using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitBooleanNegateExpressionConverter : AutoitInvocationExpressionConverterBase<BooleanNegateExpression>
    {
        public AutoitBooleanNegateExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( BooleanNegateExpression node, IContextService contextService ) {
            string runtimeInstanceName = contextService.GetRuntimeInstanceName();
            string compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.NOT( null ) );
            IEnumerable<ArgumentSyntax> parameter = CreateParameter( node.Left.ToEnumerable(), contextService );

            return CreateInvocationExpression( runtimeInstanceName, compilerFunctionName, parameter );
        }
    }
}
