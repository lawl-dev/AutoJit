using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.AST.Parser.Interface
{
	public interface IStatementParser
	{
		List<IStatementNode> ParseBlock( TokenQueue block );
	}
}
