using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitExitloopStatementConverter : AutoitStatementConverterBase<ExitloopStatement>
    {
        public AutoitExitloopStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService )
        : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ExitloopStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            string exitLoopLabelName = context.GetExitLoopLabelName( statement.Level );

            GotoStatementSyntax gotoStatement = SyntaxFactory.GotoStatement( SyntaxKind.GotoStatement, SyntaxFactory.IdentifierName( exitLoopLabelName ) );

            toReturn.Add( gotoStatement );

            return toReturn;
        }
    }
}
