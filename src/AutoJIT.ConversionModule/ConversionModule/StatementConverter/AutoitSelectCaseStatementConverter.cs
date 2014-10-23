using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitSelectCaseStatementConverter : AutoitStatementConverterBase<SelectCaseStatement>
    {
        public AutoitSelectCaseStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( SelectCaseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();
            context.RegisterSelectSwitch();

            IfStatementSyntax[] ifStatementSyntaxs =
                statement.Cases.Select(
                    ( @case, index ) => {
                        context.RegisterCase();
                        var block = @case.Value.SelectMany( x => ConvertGeneric( x, context ) ).ToList();

                        var continueCaseLabelName = context.GetContinueCaseLabelName( index );
                        //var continueCaseLabel = SyntaxFactory.LabeledStatement( continueCaseLabelName, SyntaxFactory.EmptyStatement() );

                        var format = string.Format( "JUMPABHACK_{0}", continueCaseLabelName );
                        var statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                            "Console", "WriteLine",
                            new[] {
                                new CSharpParameterInfo( SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,SyntaxFactory.Literal( format, format) ), false ),
                            } ).ToStatementSyntax();


                        block.Insert(0, statementSyntax);

                        var ifStatementSyntax = CSharpStatementFactory.CreateIfStatement( Convert( @case.Key, context ), block );
                        return ifStatementSyntax;
                    }
                    ).ToArray();



            if ( statement.Else.Any() ) {
                context.RegisterCase();

                var block = statement.Else.SelectMany( x => ConvertGeneric( x, context ) ).ToList();

                var continueCaseLabelName = context.GetContinueCaseLabelName(statement.Cases.Count + 1);
                
                
                
                //var continueCaseLabel = SyntaxFactory.LabeledStatement(continueCaseLabelName, SyntaxFactory.EmptyStatement());

                var format = string.Format("JUMPABHACK_{0}", continueCaseLabelName);

                var statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                "Console", "WriteLine",
                new[] { new CSharpParameterInfo(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(SyntaxFactory.TriviaList(), format, format, SyntaxFactory.TriviaList())), false), }).ToStatementSyntax();




                block.Insert(0, statementSyntax);



                ifStatementSyntaxs[ifStatementSyntaxs.Length-1] = ifStatementSyntaxs[ifStatementSyntaxs.Length-1].WithElse(
                    SyntaxFactory.ElseClause(
                        SyntaxFactory.Block( block ) ) );

            }
            for ( int i = ifStatementSyntaxs.Length-1; i > 0; i-- ) {
                ifStatementSyntaxs[i-1] = ifStatementSyntaxs[i-1].WithElse( SyntaxFactory.ElseClause( ifStatementSyntaxs[i] ) );
            }
            toReturn.Add( ifStatementSyntaxs[0] );

            context.UnregisterSelectSwitch();
            return toReturn;
        }
    }
}
