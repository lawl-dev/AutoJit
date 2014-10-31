using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
	public sealed class VariableFunctionCallStatementParserStrategy : StatementParserStrategyBase<VariableFunctionCallStatement>
	{
		public VariableFunctionCallStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}
		
		
		public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
			var variableFunctionCallExpression = ExpressionParser.ParseSingle<VariableFunctionCallExpression>( block );

			return new VariableFunctionCallStatement( variableFunctionCallExpression ).ToEnumerable();
		}
	}
}