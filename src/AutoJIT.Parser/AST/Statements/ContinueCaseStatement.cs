using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class ContinueCaseStatement : StatementBase
	{
		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

		public override string ToSource() {
			return "ContinueCase";
		}

		public override object Clone() {
			return new ContinueCaseStatement();
		}
	}
}
