using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitFunctionCallStatementConverter : AutoitStatementConverterBase<FunctionCallStatement>
    {
        public AutoitFunctionCallStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(FunctionCallStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            toReturn.Add( Convert(statement.FunctionCallExpression, context).ToStatementSyntax() );

            return toReturn;
        }
    }
}
