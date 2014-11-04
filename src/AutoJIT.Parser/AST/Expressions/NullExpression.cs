using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class NullExpression : ExpressionBase
	{
		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitNullExpression( this );
	    }

	    public override string ToSource() {
			return "Null";
		}

		public override object Clone() {
			return new NullExpression();
		}
	}
}
