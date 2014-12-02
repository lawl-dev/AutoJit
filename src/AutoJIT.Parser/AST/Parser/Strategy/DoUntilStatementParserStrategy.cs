using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class DoUntilStatementParserStrategy : StatementParserStrategyBase<DoUntilStatement>
    {
        public DoUntilStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseDoUntil( block ).ToEnumerable();
        }

        private DoUntilStatement ParseDoUntil( TokenQueue block ) {
            TokenCollection doWhileBlockToken = ParseDoWhileBlock( block );
            TokenCollection doWhileExpressionToken = ParseWhileExpression( block );

            IExpressionNode doWHileExpression = ExpressionParser.ParseBlock( doWhileExpressionToken, true );
            List<IStatementNode> doWhileBlockStatements = StatementParser.ParseBlock( doWhileBlockToken );

            return AutoitSyntaxFactory.CreateDoUntilStatement( doWHileExpression, doWhileBlockStatements );
        }

        private TokenCollection ParseDoWhileBlock( TokenQueue block ) {
            return GetBetween( block, Keywords.Do, Keywords.Until, true );
        }

        private TokenCollection ParseWhileExpression(TokenQueue block)
        {
            var whileExpression = new TokenCollection(block.DequeueWhile(x => x.Type != TokenType.NewLine).Where(x => x.Value.Keyword != Keywords.While).ToList());

            return whileExpression;
        }
    }
}
