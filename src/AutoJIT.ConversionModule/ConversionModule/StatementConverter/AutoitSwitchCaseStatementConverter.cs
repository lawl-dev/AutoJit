using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
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

            var toFunctionName = CompilerHelper.GetCompilerMemberName( x => x.To( null, null, null ) );
            var equalFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Equal( null, null ) );

            var conditions = new List<ExpressionSyntax>();

            foreach (var @case in statement.Cases) {
                if ( @case.Key.Count() > 1 ) {
                    var conditionParameterExpression = @case.Key.Select( x => Convert( x, context ) )
                        .Concat( new[] { Convert( statement.Condition, context ) } )
                        .ToArray();

                    var caseExpression = CSharpStatementFactory.CreateInvocationExpression(
                        context.GetRuntimeInstanceName(), toFunctionName, CompilerHelper.GetParameterInfo( toFunctionName, conditionParameterExpression ) );
                    conditions.Add( caseExpression );
                }
                else {
                    var caseExpression = CSharpStatementFactory.CreateInvocationExpression(
                        context.GetRuntimeInstanceName(), equalFunctionName,
                        CompilerHelper.GetParameterInfo(
                            equalFunctionName, Convert( statement.Condition, context ),
                            Convert( @case.Key.Single(), context ) ) );
                    conditions.Add( caseExpression );
                }
            }

            var ifs = new List<IfStatementSyntax>();
            for ( int i = conditions.Count-1; i >= 0; i-- ) {
                var currentCase = statement.Cases.Skip( i ).First();
                var elseIf = CSharpStatementFactory.CreateIfStatement(
                    conditions[i], currentCase.Value.SelectMany( x => ConvertGeneric( x, context ) ) );
                ifs.Add( elseIf );
            }
            ifs.Reverse();

            if ( statement.Else != null ) {
                ifs[ifs.Count-1] =
                    ifs[ifs.Count-1].WithElse(
                        SyntaxFactory.ElseClause(
                            SyntaxFactory.Block( statement.Else.SelectMany( x => ConvertGeneric( x, context ) ) ) ) );
            }

            for ( int i = ifs.Count-1; i > 0; i-- ) {
                ifs[i-1] = ifs[i-1].WithElse( SyntaxFactory.ElseClause( ifs[i] ) );
            }

            toReturn.Add( ifs[0] );

            return toReturn;
        }
    }
}
