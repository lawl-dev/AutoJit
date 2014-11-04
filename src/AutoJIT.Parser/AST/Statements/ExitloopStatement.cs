using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class ExitloopStatement : StatementBase
	{
		public ExitloopStatement( int level ) {
			Level = level;
		}

		public int Level { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitExitloopStatement( this );
	    }

	    public override string ToSource() {
			return string.Format( "ExitLoop {0}", Level );
		}

		public override object Clone() {
			return new ExitloopStatement( Level );
		}

	    public ExitloopStatement Update( int level ) {
	        if ( Level == level ) {
	            return this;
	        }
            return new ExitloopStatement( level );
	    }
	}
}
