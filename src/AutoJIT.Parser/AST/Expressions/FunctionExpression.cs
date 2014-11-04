using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

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

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitFunctionExpression( this );
	    }

	    public override string ToSource() {
			return IdentifierName;
		}

		public override object Clone() {
			return new FunctionExpression( IdentifierName );
		}

	    public FunctionExpression Update( string identifierName ) {
	        if ( IdentifierName == identifierName ) {
	            return this;
	        }
            return new FunctionExpression( identifierName );
	    }
	}
}