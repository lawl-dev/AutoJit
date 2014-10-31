using AutoJIT.Parser.AST.Expressions.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter.Interface
{
	public interface IAutoitExpressionConverter<in TInExpression, out TOutExpression> : IAutoitExpressionConverter<TOutExpression>
	where TInExpression : IExpressionNode
	{
		TOutExpression Convert( TInExpression node, IContextService contextService );
	}

	public interface IAutoitExpressionConverter<out TOutExpression>
	{
		TOutExpression ConverGeneric( IExpressionNode node, IContextService contextService );
		ExpressionSyntax Convert( IExpressionNode node, IContextService contextService );
	}
}
