using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitForInStatementConverter : AutoitStatementConverterBase<ForInStatement>
    {
        public AutoitForInStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(ForInStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();
            toReturn.Add(
                CSharpStatementFactory.CreateForInStatement(
                    statement.VariableExpression.IdentifierName, Convert( statement.ToEnumerate, context),
                    statement.Block.SelectMany( x => ConvertGeneric(x, context) ) ) );

            return toReturn;
        }
    }
}
