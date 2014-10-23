using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitContinueCaseStatementConverter : AutoitStatementConverterBase<ContinueCaseStatement>
    {
        public AutoitContinueCaseStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ContinueCaseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            string continueLoopLabelName = context.GetContinueCaseLabelName();
            //toReturn.Add(SyntaxFactory.GotoStatement(SyntaxKind.GotoStatement, SyntaxFactory.IdentifierName(continueLoopLabelName)));

            var statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                "Console", "WriteLine",
                new[] {
                    new CSharpParameterInfo(
                        SyntaxFactory.LiteralExpression(
                            SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal( string.Format( "JUMPTOHACK_{0}", continueLoopLabelName ) ) ), false ),
                } ).ToStatementSyntax();

            toReturn.Add(
                statementSyntax );

            return toReturn;
        }
    }
}
