using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class ExitStatement : StatementBase
	{
		public ExitStatement( IExpressionNode exitExpression ) {
			ExitExpression = exitExpression;
			Initialize();
		}

		public IExpressionNode ExitExpression { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return new List<ISyntaxNode> {
					ExitExpression
				};
			}
		}

		public override string ToSource() {
			return string.Format( "Exit {0}", ExitExpression.ToSource() );
		}

		public override object Clone() {
			return new ExitStatement( (IExpressionNode)ExitExpression.Clone() );
		}
	}
}
