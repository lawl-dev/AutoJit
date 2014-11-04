using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class SwitchCase : StatementBase
	{
		public SwitchCase( List<CaseCondition> conditions, List<IStatementNode> block ) {
			Conditions = conditions;
			Block = block;
			Initialize();
		}

		public List<CaseCondition> Conditions { get; private set; }
		public List<IStatementNode> Block { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var nodes = new List<ISyntaxNode>();

				nodes.AddRange( Conditions );
				nodes.AddRange( Block );

				return nodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitSwitchCase( this );
	    }

	    public override string ToSource() {
			string toReturn = string.Empty;
			foreach(CaseCondition condition in Conditions) {
				toReturn += condition.ToSource();
			}
			return toReturn;
		}

		public override object Clone() {
			return new SwitchCase( Conditions.Select( x => (CaseCondition)x.Clone() ).ToList(), Block.Select( x => (IStatementNode)x.Clone() ).ToList() );
		}
	}
}
