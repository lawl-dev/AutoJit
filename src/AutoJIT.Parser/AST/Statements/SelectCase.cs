using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class SelectCase : StatementBase
	{
		public SelectCase( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
			Condition = condition;
			Block = block;
			Initialize();
		}

		public IExpressionNode Condition { get; set; }
		public IEnumerable<IStatementNode> Block { get; set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var nodes = new List<ISyntaxNode>();

				nodes.Add( Condition );
				nodes.AddRange( Block );

				return nodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitSelectCase( this );
	    }

	    public override string ToSource() {
			string toReturn = string.Empty;
			toReturn += string.Format( "Case {0}{1}", Condition.ToSource(), Environment.NewLine );
			foreach(IStatementNode node in Block) {
				toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
			}
			return toReturn;
		}

		public override object Clone() {
			return new SelectCase( (IExpressionNode)Condition.Clone(), Block.Select( x => (IStatementNode)x.Clone() ).ToList() );
		}

	    public SelectCase Update( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
	        if ( Condition == condition &&
	             Block == block ) {
	            return this;
	        }
            return new SelectCase( condition, block );
	    }
	}
}
