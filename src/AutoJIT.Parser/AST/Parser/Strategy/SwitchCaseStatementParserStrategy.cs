using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class SwitchCaseStatementParserStrategy : StatementParserStrategyBase<SwitchCaseStatement>
    {
        public SwitchCaseStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseSwitch( block ).ToEnumerable();
        }

        private SwitchCaseStatement ParseSwitch( TokenQueue block ) {
            var expression = ParseUntilNewLine( block );

            var condition = ExpressionParser.ParseBlock( expression, true );
            SkipAndAssert( block, TokenType.NewLine );
            var cases = new Dictionary<IEnumerable<IExpressionNode>, IEnumerable<IStatementNode>>();
            IEnumerable<IStatementNode> @else = null;
            SkipAndAssert( block, Keywords.Case );

            while ( block.Peek().Value.Keyword != Keywords.EndSwitch ) {
                var line = ParseUntilNewLine( block );
                if ( line.Any( x => x.Value.Keyword == Keywords.To ) ) {
                    var parts = line.Split( x => x.Value.Keyword == Keywords.To );
                    if ( parts.Count != 2 ) {
                        throw new SyntaxTreeException( "Unexpected To in statement", parts[0][0].Col, parts[0][0].Line );
                    }
                    var left = ExpressionParser.ParseBlock( new TokenCollection( parts.First() ), true );
                    var right = ExpressionParser.ParseBlock( new TokenCollection( parts.Skip( 1 ).First() ), true );

                    var caseBlock = ParseInnerUntilSwitchSelect( block );
                    cases.Add( Utils.GetEnumerable( left, right ), StatementParser.ParseBlock( caseBlock ).ToList() );
                }
                else if ( line.Any() &&
                          line[0].Value.Keyword != Keywords.Else ) {
                    var left = ExpressionParser.ParseBlock( line, true );
                    var caseBlock = ParseInnerUntilSwitchSelect( block );
                    cases.Add( left.ToEnumerable(), StatementParser.ParseBlock( caseBlock ).ToList() );
                }
                else {
                    if ( line[0].Value.Keyword != Keywords.Else ) {
                        throw new SyntaxTreeException( "Unexpected token", line[0].Col, line[0].Line );
                    }
                    var elseTokens = ParseInnerUntilSwitchSelect( block );
                    @else = StatementParser.ParseBlock( elseTokens ).ToList();
                }
                if ( block.Peek().Value.Keyword != Keywords.EndSwitch ) {
                    SkipAndAssert( block, Keywords.Case );
                }
            }
            SkipAndAssert( block, Keywords.EndSwitch );
            return new SwitchCaseStatement( condition, cases, @else );
        }
    }
}
