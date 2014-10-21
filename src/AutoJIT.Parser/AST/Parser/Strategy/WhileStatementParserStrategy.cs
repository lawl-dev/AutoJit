using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class WhileStatementParserStrategy : StatementParserStrategyBase<WhileStatement>
    {
        public WhileStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseWhile( block ).ToEnumerable();
        }

        private WhileStatement ParseWhile( TokenQueue block ) {
            TokenCollection whileExpressionToken = ParseWhileExpression( block );
            TokenCollection whileBlock = ParseWhileBlock( block );

            IExpressionNode whileExpression = ExpressionParser.ParseBlock( whileExpressionToken, true );
            List<IStatementNode> whileBlockStatements = StatementParser.ParseBlock( whileBlock );

            return AutoitStatementFactory.CreateWhileStatement( whileExpression, whileBlockStatements );
        }
    }
}
