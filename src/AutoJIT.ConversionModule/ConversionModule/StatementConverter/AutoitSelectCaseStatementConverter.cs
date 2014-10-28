using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitSelectCaseStatementConverter : AutoitStatementConverterBase<SelectCaseStatement>
    {
        public AutoitSelectCaseStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService )
        : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( SelectCaseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();
            context.RegisterSelectSwitch();

            IfStatementSyntax[] ifStatementSyntaxs = statement.Cases.Select(
                                                                            ( @case, index ) => {
                                                                                context.RegisterCase();
                                                                                List<StatementSyntax> block =
                                                                                @case.Value.SelectMany( x => ConvertGeneric( x, context ) ).ToList();

                                                                                string continueCaseLabelName = context.GetContinueCaseLabelName();

                                                                                string format = string.Format( "JUMPABHACK_{0}", continueCaseLabelName );
                                                                                StatementSyntax statementSyntax =
                                                                                CSharpStatementFactory.CreateInvocationExpression(
                                                                                                                                  "Console",
                                                                                                                                  "WriteLine",
                                                                                                                                  new[] {
                                                                                                                                      new CSharpParameterInfo(
                                                                                                                                  SyntaxFactory
                                                                                                                                  .LiteralExpression(
                                                                                                                                                     SyntaxKind
                                                                                                                                                     .StringLiteralExpression,
                                                                                                                                                     SyntaxFactory
                                                                                                                                                     .Literal(
                                                                                                                                                              format,
                                                                                                                                                              format ) ),
                                                                                                                                  false )
                                                                                                                                  } ).ToStatementSyntax();

                                                                                block.Insert( 0, statementSyntax );

                                                                                IfStatementSyntax ifStatementSyntax =
                                                                                CSharpStatementFactory.CreateIfStatement( Convert( @case.Key, context ), block );
                                                                                return ifStatementSyntax;
                                                                            } ).ToArray();

            if( statement.Else.Any() ) {
                context.RegisterCase();

                List<StatementSyntax> block = statement.Else.SelectMany( x => ConvertGeneric( x, context ) ).ToList();

                string continueCaseLabelName = context.GetContinueCaseLabelName( 1 );

                //var continueCaseLabel = SyntaxFactory.LabeledStatement(continueCaseLabelName, SyntaxFactory.EmptyStatement());

                string format = string.Format( "JUMPABHACK_{0}", continueCaseLabelName );

                StatementSyntax statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                                                                                                    "Console",
                                                                                                    "WriteLine",
                                                                                                    new[] {
                                                                                                        new CSharpParameterInfo(
                                                                                                    SyntaxFactory.LiteralExpression(
                                                                                                                                    SyntaxKind
                                                                                                                                    .StringLiteralExpression,
                                                                                                                                    SyntaxFactory.Literal(
                                                                                                                                                          format,
                                                                                                                                                          format ) ),
                                                                                                    false )
                                                                                                    } ).ToStatementSyntax();

                block.Insert( 0, statementSyntax );

                ifStatementSyntaxs[ifStatementSyntaxs.Length-1] =
                ifStatementSyntaxs[ifStatementSyntaxs.Length-1].WithElse( SyntaxFactory.ElseClause( SyntaxFactory.Block( block ) ) );
            }
            for( int i = ifStatementSyntaxs.Length-1; i > 0; i-- ) {
                ifStatementSyntaxs[i-1] = ifStatementSyntaxs[i-1].WithElse( SyntaxFactory.ElseClause( ifStatementSyntaxs[i] ) );
            }
            toReturn.Add( ifStatementSyntaxs[0] );

            context.UnregisterSelectSwitch();
            return toReturn;
        }
    }
}
