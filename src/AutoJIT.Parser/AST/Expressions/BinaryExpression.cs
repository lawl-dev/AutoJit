using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class BinaryExpression : ExpressionBase
	{
		public BinaryExpression( IExpressionNode left, IExpressionNode right, Token @operator ) {
			Operator = @operator;
			Right = right;
			Left = left;
			Initialize();
		}

		public IExpressionNode Left { get; private set; }

		public IExpressionNode Right { get; private set; }

		public Token Operator { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var toReturn = new List<ISyntaxNode> {
					Left,
					Right
				};
				return toReturn;
			}
		}

		public override string ToSource() {
			return string.Format( "{0} {1} {2}", Left.ToSource(), Operator, Right.ToSource() );
		}

		public override object Clone() {
			return new BinaryExpression( (IExpressionNode)Left.Clone(), (IExpressionNode)Right.Clone(), Operator );
		}
	}
}
