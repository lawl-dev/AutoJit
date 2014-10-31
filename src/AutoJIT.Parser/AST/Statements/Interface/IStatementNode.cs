using System.Collections.Generic;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements.Interface
{
	public interface IStatementNode : ISyntaxNode
	{
		IEnumerable<TReturn> Accpet<TReturn>( IStatementVisitor<IEnumerable<TReturn>> visitor );
	}
}
