using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Visitor
{
	public interface IExpressionVisitor<out TReturn> : IExpressionSyntaxVisitor<IExpressionNode, TReturn> {}
}
