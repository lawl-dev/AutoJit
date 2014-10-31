using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class NegateExpression : ExpressionBase
	{
		public NegateExpression( IExpressionNode expressionNode ) {
			ExpressionNode = expressionNode;
			Initialize();
		}

		public IExpressionNode ExpressionNode { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return ExpressionNode.ToEnumerable();
			}
		}

		public override string ToSource() {
			return string.Format( "-{0}", ExpressionNode.ToSource() );
		}

		public override object Clone() {
			return new NegateExpression( (IExpressionNode)ExpressionNode.Clone() );
		}
	}
}
