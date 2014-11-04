using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
	public class CaseCondition : ExpressionBase
	{
		public CaseCondition( IExpressionNode left, IExpressionNode right ) {
			Left = left;
			Right = right;
		}

		public IExpressionNode Left { get; private set; }
		public IExpressionNode Right { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var nodes = new List<ISyntaxNode>();
				nodes.Add( Left );
				if( Right != null ) {
					nodes.Add( Right );
				}

				return nodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitCaseCondition( this );
	    }

	    public override string ToSource() {
			if( Right == null ) {
				return Left.ToSource();
			}
			return string.Format( "{0} To {1}", Left.ToSource(), Right.ToSource() );
		}

		public override object Clone() {
			return new CaseCondition( (IExpressionNode)Left.Clone(), (IExpressionNode)Right.Clone() );
		}

	    public CaseCondition Update( IExpressionNode left, IExpressionNode right ) {
	        if ( Left == left &&
	             Right == right ) {
	            return this;
	        }
            return new CaseCondition( left, right );
	    }
	}
}
