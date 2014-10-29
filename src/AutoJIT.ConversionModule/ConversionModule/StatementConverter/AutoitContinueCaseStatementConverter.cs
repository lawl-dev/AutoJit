using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitContinueCaseStatementConverter : AutoitStatementConverterBase<ContinueCaseStatement>
    {
        public AutoitContinueCaseStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService )
        : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ContinueCaseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            string continueCaseLabelName = context.GetContinueCaseLabelName( 1 );

            StatementSyntax statementSyntax = GetPlaceholder( continueCaseLabelName );

            toReturn.Add( statementSyntax );

            return toReturn;
        }

        private StatementSyntax GetPlaceholder( string continueCaseLabelName ) {
            return CSharpStatementFactory.CreateInvocationExpression(
                                                                     "Console",
                                                                     "WriteLine",
                                                                     new[] {
                                                                         new CSharpParameterInfo(
                                                                     SyntaxFactory.LiteralExpression(
                                                                                                     SyntaxKind.StringLiteralExpression,
                                                                                                     SyntaxFactory.Literal(
                                                                                                                           string.Format(
                                                                                                                                         "JUMPTOHACK_{0}",
                                                                                                                                         continueCaseLabelName ) ) ),
                                                                     false )
                                                                     } ).ToStatementSyntax();
        }
    }
}
