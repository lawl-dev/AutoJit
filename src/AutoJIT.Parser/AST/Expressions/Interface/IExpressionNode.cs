using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions.Interface
{
	public interface IExpressionNode : ISyntaxNode
	{
		TReturn Accpet<TReturn>( IExpressionVisitor<TReturn> visitor );
	}
}
