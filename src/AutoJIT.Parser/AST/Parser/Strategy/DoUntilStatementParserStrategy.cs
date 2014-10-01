using System.Collections.Generic;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class DoUntilStatementParserStrategy : StatementParserStrategyBase<DoUntilStatement>
    {
        public DoUntilStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseDoUntil( block ).ToEnumerable();
        }

        private DoUntilStatement ParseDoUntil( TokenQueue block ) {
            var doWhileBlockToken = ParseDoWhileBlock( block );
            var doWhileExpressionToken = ParseWhileExpression( block );

            var doWHileExpression = ExpressionParser.ParseBlock( doWhileExpressionToken, true );
            var doWhileBlockStatements = StatementParser.ParseBlock( doWhileBlockToken );

            return AutoitStatementFactory.CreateDoUntilStatement( doWHileExpression, doWhileBlockStatements );
        }

        private TokenCollection ParseDoWhileBlock( TokenQueue block ) {
            return ParseInner( block, Keywords.Do, Keywords.Until, true );
        }
    }
}
