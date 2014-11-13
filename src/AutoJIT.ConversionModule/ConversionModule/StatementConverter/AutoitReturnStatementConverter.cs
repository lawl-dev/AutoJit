using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Parser.AST.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitReturnStatementConverter : AutoitStatementConverterBase<ReturnStatement>
    {
        public AutoitReturnStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ReturnStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            toReturn.Add( CSharpStatementFactory.CreateReturn( ConvertGeneric( statement.ReturnExpression, context ) ) );

            return toReturn;
        }
    }
}
