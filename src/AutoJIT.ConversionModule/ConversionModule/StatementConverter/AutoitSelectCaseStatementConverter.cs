using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitSelectCaseStatementConverter : AutoitStatementConverterBase<SelectCaseStatement>
    {
        public AutoitSelectCaseStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(SelectCaseStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            var ifStatementSyntaxs =
                statement.Cases.Select(
                    @case =>
                        CSharpStatementFactory.CreateIfStatement(
                            Convert(@case.Key, context),
                            @case.Value.SelectMany( x => ConvertGeneric(x,context) ) )).ToArray();
            if ( statement.Else.Any() ) {
                ifStatementSyntaxs[ifStatementSyntaxs.Length-1] = ifStatementSyntaxs[ifStatementSyntaxs.Length-1].WithElse(
                    SyntaxFactory.ElseClause(
                        SyntaxFactory.Block( statement.Else.SelectMany( x => ConvertGeneric(x, context) ) ) ) );
            }
            for ( int i = ifStatementSyntaxs.Length-1; i > 0; i-- ) {
                ifStatementSyntaxs[i-1] = ifStatementSyntaxs[i-1].WithElse( SyntaxFactory.ElseClause( ifStatementSyntaxs[i] ) );
            }
            toReturn.Add( ifStatementSyntaxs[0] );

            return toReturn;
        }
    }
}
