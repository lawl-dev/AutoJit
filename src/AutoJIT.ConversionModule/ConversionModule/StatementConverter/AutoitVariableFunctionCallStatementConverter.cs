using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitVariableFunctionCallStatementConverter : AutoitStatementConverterBase<VariableFunctionCallStatement>
    {
        public AutoitVariableFunctionCallStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( VariableFunctionCallStatement statement, IContextService context ) {
            ExpressionSyntax toReturn = ConvertGeneric( statement.VariableFunctionCallExpression, context );

            return toReturn.ToStatementSyntax().ToEnumerable();
        }
    }
}
