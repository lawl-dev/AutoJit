using System.Collections.Generic;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Parser.AST.Statements;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitExitloopStatementConverter : AutoitStatementConverterBase<ExitloopStatement>
    {
        public AutoitExitloopStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ExitloopStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            string exitLoopLabelName = context.GetExitLoopLabelName(
                statement.Level == null
                    ? 1
                    : statement.Level.Token.Value.Int32Value );

            GotoStatementSyntax gotoStatement = SyntaxFactory.GotoStatement( SyntaxKind.GotoStatement, SyntaxFactory.IdentifierName( exitLoopLabelName ) );

            toReturn.Add( gotoStatement );

            return toReturn;
        }
    }
}
