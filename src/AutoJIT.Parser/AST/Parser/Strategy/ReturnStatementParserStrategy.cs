using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ReturnStatementParserStrategy : StatementParserStrategyBase<ReturnStatement>
    {
        public ReturnStatementParserStrategy(
        IStatementParser statementParser,
        IExpressionParser expressionParser,
        IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseReturn( block ).ToEnumerable();
        }

        private ReturnStatement ParseReturn( TokenQueue block ) {
            TokenCollection returnExpressionTokenCollection = ParseUntilNewLine( block );

            IExpressionNode returnExpression = ExpressionParser.ParseBlock( returnExpressionTokenCollection, true );

            if( returnExpression == null ) {
                returnExpression = new NullExpression();
            }

            return AutoitStatementFactory.CreateReturnStatement( returnExpression );
        }
    }
}
