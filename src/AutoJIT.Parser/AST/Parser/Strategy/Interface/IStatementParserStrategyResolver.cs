using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy.Interface
{
	public interface IStatementParserStrategyResolver
	{
		IStatementParserStrategy<TStatement> Resolve<TStatement>() where TStatement : IStatementNode;
	}
}
