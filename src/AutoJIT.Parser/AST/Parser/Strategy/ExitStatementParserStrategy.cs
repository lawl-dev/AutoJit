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
    public sealed class ExitStatementParserStrategy : StatementParserStrategyBase<ExitStatement>
    {
        public ExitStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseExit( block ).ToEnumerable();
        }

        private ExitStatement ParseExit( TokenQueue block ) {
            TokenCollection exitExpression = ParseUntilNewLine( block );
            IExpressionNode expressionNode = ExpressionParser.ParseBlock( exitExpression, true );
            return AutoitStatementFactory.CreateExitStatement( expressionNode );
        }
    }
}
