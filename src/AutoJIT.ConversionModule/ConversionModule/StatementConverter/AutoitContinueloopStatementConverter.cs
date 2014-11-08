using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitContinueloopStatementConverter : AutoitStatementConverterBase<ContinueLoopStatement>
    {
        public AutoitContinueloopStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ContinueLoopStatement statement, IContextService context ) {
            string coninueLoopLabelName = context.GetConinueLoopLabelName(
                statement.Level == null
                    ? 1
                    : statement.Level.Token.Value.Int32Value );

            GotoStatementSyntax toReturn = SyntaxFactory.GotoStatement( SyntaxKind.GotoStatement, SyntaxFactory.IdentifierName( coninueLoopLabelName ) );

            return toReturn.ToEnumerable();
        }
    }
}
