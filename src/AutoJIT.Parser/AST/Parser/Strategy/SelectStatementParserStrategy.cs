using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class SelectStatementParserStrategy : StatementParserStrategyBase<SelectCaseStatement>
    {
        public SelectStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseSelect( block ).ToEnumerable();
        }

        private SelectCaseStatement ParseSelect( TokenQueue block ) {
            SkipAndAssert( block, TokenType.NewLine );
            var cases = new Dictionary<IExpressionNode, IEnumerable<IStatementNode>>();

            IEnumerable<IStatementNode> elseStatements = new List<IStatementNode>();
            while ( block.Peek().Value.Keyword != Keywords.Endselect ) {
                SkipAndAssert( block, Keywords.Case );

                if ( block.Peek().Value.Keyword != Keywords.Else ) {
                    TokenCollection expression = ParseUntilNewLine( block );
                    TokenCollection caseBlock = ParseInnerUntilSwitchSelect( block );

                    IExpressionNode caseCondition = ExpressionParser.ParseBlock( expression, true );
                    List<IStatementNode> caseStatements = StatementParser.ParseBlock( caseBlock );
                    cases.Add( caseCondition, caseStatements );
                }
                else {
                    SkipAndAssert( block, Keywords.Else );
                    TokenCollection elseBlock = ParseInnerUntil( block, Keywords.Select, Keywords.Endselect, true );

                    elseStatements = StatementParser.ParseBlock( elseBlock );
                }
            }
            SkipAndAssert( block, Keywords.Endselect );
            return AutoitStatementFactory.CreateSelectStatement( cases, elseStatements );
        }
    }
}
