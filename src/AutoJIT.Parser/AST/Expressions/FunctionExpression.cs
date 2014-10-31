using System.Collections.Generic;
using System.Linq;

namespace AutoJIT.Parser.AST.Expressions
{
	public class FunctionExpression : ExpressionBase
	{
		public string IdentifierName { get; private set; }

		public FunctionExpression(string identifierName) {
			IdentifierName = identifierName;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

		public override string ToSource() {
			return IdentifierName;
		}

		public override object Clone() {
			return new FunctionExpression( IdentifierName );
		}
	}
}