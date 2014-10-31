using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class SelectCaseStatement : StatementBase
	{
		public SelectCaseStatement( IEnumerable<SelectCase> cases, IEnumerable<IStatementNode> @else ) {
			Cases = cases;
			Else = @else;
			Initialize();
		}

		public IEnumerable<SelectCase> Cases { get; private set; }
		public IEnumerable<IStatementNode> Else { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var syntaxNodes = new List<ISyntaxNode>();
				syntaxNodes.AddRange( Cases );

				if( Else != null ) {
					syntaxNodes.AddRange( Else );
				}

				return syntaxNodes;
			}
		}

		public override string ToSource() {
			string toReturn = string.Format( "Select{0}", Environment.NewLine );
			foreach(SelectCase @case in Cases) {
				toReturn += string.Format( "{0}{1}", @case.ToSource(), Environment.NewLine );
			}

			if( Else != null ) {
				toReturn += string.Format( "Case Else{0}", Environment.NewLine );
				foreach(IStatementNode elseStatement in Else) {
					toReturn += string.Format( "{0}{1}", elseStatement.ToSource(), Environment.NewLine );
				}
			}

			toReturn += "EndSelect";
			return toReturn;
		}

		public override object Clone() {
			return new SelectCaseStatement( Cases.Select( x => (SelectCase)x.Clone() ).ToList(), CloneEnumerableAs<IStatementNode>( Else ) );
		}
	}
}
