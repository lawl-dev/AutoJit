using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
	public sealed class RedimStatementParserStrategy : StatementParserStrategyBase<ReDimStatement>
	{
		public RedimStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

		public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
			return ParseRedim( block );
		}

		private IEnumerable<ReDimStatement> ParseRedim( TokenQueue block ) {
			var toReturn = new List<ReDimStatement>();
			do {
				Consume( block, TokenType.Comma );
				var arrayExpression = ExpressionParser.ParseSingle<ArrayExpression>( block );

				toReturn.Add( AutoitStatementFactory.CreateReDimStatement( arrayExpression ) );
			} while( block.Any()
					 && block.Peek().Type == TokenType.Comma );
			return toReturn;
		}
	}
}
