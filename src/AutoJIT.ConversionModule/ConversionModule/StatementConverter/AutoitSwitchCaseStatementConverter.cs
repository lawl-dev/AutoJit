using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitSwitchCaseStatementConverter : AutoitStatementConverterBase<SwitchCaseStatement>
    {
        public AutoitSwitchCaseStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( SwitchCaseStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();
            context.RegisterSelectSwitch();

            string toFunctionName = CompilerHelper.GetCompilerMemberName( x => x.To( null, null, null ) );
            string equalFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Equal( null, null ) );

            var conditions = new List<ExpressionSyntax>();

            foreach (var @case in statement.Cases) {
                if ( @case.Key.Count() > 1 ) {
                    ExpressionSyntax[] conditionParameterExpression = @case.Key.Select( x => Convert( x, context ) )
                        .Concat( new[] { Convert( statement.Condition, context ) } )
                        .ToArray();

                    InvocationExpressionSyntax caseExpression = CSharpStatementFactory.CreateInvocationExpression(
                        context.GetRuntimeInstanceName(), toFunctionName, CompilerHelper.GetParameterInfo( toFunctionName, conditionParameterExpression ) );
                    conditions.Add( caseExpression );
                }
                else {
                    InvocationExpressionSyntax caseExpression = CSharpStatementFactory.CreateInvocationExpression(
                        context.GetRuntimeInstanceName(), equalFunctionName,
                        CompilerHelper.GetParameterInfo(
                            equalFunctionName, Convert( statement.Condition, context ),
                            Convert( @case.Key.Single(), context ) ) );
                    conditions.Add( caseExpression );
                }
            }

            var ifs = new List<IfStatementSyntax>();

            for ( int i = 0; i < conditions.Count; i++ ) {
                KeyValuePair<IEnumerable<IExpressionNode>, IEnumerable<IStatementNode>> currentCase = statement.Cases.Skip(i).First();
                context.RegisterCase();
                
                var continueCaseLabelName = context.GetContinueCaseLabelName();

                
                var format = string.Format("JUMPABHACK_{0}", continueCaseLabelName);
                var statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                    "Console", "WriteLine",
                    new[] {
                                new CSharpParameterInfo( SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,SyntaxFactory.Literal( format, format) ), false ),
                            }).ToStatementSyntax();

                var block = currentCase.Value.SelectMany(x => ConvertGeneric(x, context)).ToList();

                block.Insert(0, statementSyntax);


                IfStatementSyntax elseIf = CSharpStatementFactory.CreateIfStatement(
                    conditions[i], block);
                ifs.Add(elseIf);
            }

            if ( statement.Else != null ) {
                context.RegisterCase();

                var block = statement.Else.SelectMany( x => ConvertGeneric( x, context ) ).ToList();

                var continueCaseLabelName = context.GetContinueCaseLabelName();

                var format = string.Format("JUMPABHACK_{0}", continueCaseLabelName);
                var statementSyntax = CSharpStatementFactory.CreateInvocationExpression(
                    "Console", "WriteLine",
                    new[] {
                                new CSharpParameterInfo( SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,SyntaxFactory.Literal( format, format) ), false ),
                            }).ToStatementSyntax();

                block.Insert( 0, statementSyntax );


                ifs[ifs.Count-1] =
                    ifs[ifs.Count-1].WithElse(
                        SyntaxFactory.ElseClause(
                            SyntaxFactory.Block( block ) ) );
            }

            for ( int i = ifs.Count-1; i > 0; i-- ) {
                ifs[i-1] = ifs[i-1].WithElse( SyntaxFactory.ElseClause( ifs[i] ) );
            }

            toReturn.Add( ifs[0] );


            context.UnregisterSelectSwitch();
            return toReturn;
        }
    }
}
