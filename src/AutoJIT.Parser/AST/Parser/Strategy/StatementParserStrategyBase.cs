using System.Collections.Generic;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
	public abstract class StatementParserStrategyBase<TStatement> : StatementParserBase, IStatementParserStrategy<TStatement>
	where TStatement : IStatementNode
	{
		protected readonly IAutoitStatementFactory AutoitStatementFactory;
		protected readonly IExpressionParser ExpressionParser;
		protected readonly IStatementParser StatementParser;

		protected StatementParserStrategyBase( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) {
			StatementParser = statementParser;
			ExpressionParser = expressionParser;
			AutoitStatementFactory = autoitStatementFactory;
		}

		public abstract IEnumerable<IStatementNode> Parse( TokenQueue block );
	}
}
