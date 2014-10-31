using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.AST.Parser.Strategy.Interface
{
	public interface IStatementParserStrategy
	{
		IEnumerable<IStatementNode> Parse( TokenQueue block );
	}

	public interface IStatementParserStrategy<out TStatement> : IStatementParserStrategy
	where TStatement : IStatementNode {}
}
