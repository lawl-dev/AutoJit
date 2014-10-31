using System.Collections.Generic;
using System.Linq;

namespace AutoJIT.Parser.AST.Expressions
{
	public class FalseLiteralExpression : ExpressionBase
	{
		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

		public override string ToSource() {
			return "False";
		}

		public override object Clone() {
			return new FalseLiteralExpression();
		}
	}
}
