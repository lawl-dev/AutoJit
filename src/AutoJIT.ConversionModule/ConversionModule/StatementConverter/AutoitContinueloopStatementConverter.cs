using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitContinueloopStatementConverter : AutoitStatementConverterBase<ContinueloopStatement>
    {
        public AutoitContinueloopStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ContinueloopStatement statement, IContextService context ) {
            string coninueLoopLabelName = context.GetConinueLoopLabelName( statement.Level );

            GotoStatementSyntax toReturn = SyntaxFactory.GotoStatement( SyntaxKind.GotoStatement, SyntaxFactory.IdentifierName( coninueLoopLabelName ) );

            return toReturn.ToEnumerable();
        }
    }
}
