using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitReturnStatementConverter : AutoitStatementConverterBase<ReturnStatement>
    {
        public AutoitReturnStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ReturnStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            toReturn.Add( CSharpStatementFactory.CreateReturn( Convert( statement.ReturnExpression, context ) ) );

            return toReturn;
        }
    }
}
