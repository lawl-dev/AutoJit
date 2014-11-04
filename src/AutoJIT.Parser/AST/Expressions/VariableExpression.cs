using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
	public class VariableExpression : ExpressionBase
	{
		public VariableExpression( string identifierName ) {
			IdentifierName = identifierName;
		}

		public string IdentifierName { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return Enumerable.Empty<ISyntaxNode>();
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitVariableExpression( this );
	    }

	    public override string ToSource() {
			return string.Format( "${0}", IdentifierName );
		}

		public override object Clone() {
			return new VariableExpression( (string)IdentifierName.Clone() );
		}
	}
}
