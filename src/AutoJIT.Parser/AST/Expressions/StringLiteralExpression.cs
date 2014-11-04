using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class StringLiteralExpression : LiteralExpression
	{
		public StringLiteralExpression( Token literalToken ) : base( literalToken ) {}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitStringLiteralExpression( this );
	    }

	    public override string ToSource() {
			return string.Format( "'{0}'", LiteralToken );
		}

		public override object Clone() {
			return new StringLiteralExpression( LiteralToken );
		}
	}
}
