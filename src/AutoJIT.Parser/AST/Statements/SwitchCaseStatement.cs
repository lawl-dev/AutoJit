using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class SwitchCaseStatement : StatementBase
	{
		public SwitchCaseStatement( IExpressionNode condition, List<SwitchCase> cases, IEnumerable<IStatementNode> @else ) {
			Condition = condition;
			Cases = cases;
			Else = @else;
			Initialize();
		}

		public IExpressionNode Condition { get; private set; }
		public List<SwitchCase> Cases { get; private set; }
		public IEnumerable<IStatementNode> Else { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode> {
					Condition
				};
				syntaxNodes.AddRange( Cases );
				if( Else != null ) {
					syntaxNodes.AddRange( Else );
				}

				return syntaxNodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitSwitchCaseStatement( this );
	    }

	    public override string ToSource() {
			string toReturn = string.Format( "Switch {0}{1}", Condition.ToSource(), Environment.NewLine );
			foreach(SwitchCase @case in Cases) {
				toReturn += string.Format( "{0}{1}", @case.ToSource(), Environment.NewLine );
			}
			if( Else != null ) {
				toReturn += string.Format( "Case Else{0}", Environment.NewLine );
				foreach(IStatementNode node in Else) {
					toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
				}
			}
			return toReturn;
		}

		public override object Clone() {
			return new SwitchCaseStatement( (IExpressionNode)Condition.Clone(), Cases.Select( x => (SwitchCase)x.Clone() ).ToList(), CloneEnumerableAs<IStatementNode>( Else ) );
		}
	}
}
