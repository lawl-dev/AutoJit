using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class ReDimStatement : StatementBase
	{
		public ReDimStatement( ArrayExpression arrayExpression ) {
			ArrayExpression = arrayExpression;
			Initialize();
		}

		public ArrayExpression ArrayExpression { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return new List<ISyntaxNode> {
					ArrayExpression
				};
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitReDimStatement( this );
	    }

	    public override string ToSource() {
			return string.Format( "Redim {0}", ArrayExpression.ToSource() );
		}

		public override object Clone() {
			return new ReDimStatement( (ArrayExpression)ArrayExpression.Clone() );
		}

	    public ReDimStatement Update( ArrayExpression arrayExpression ) {
	        if ( ArrayExpression == arrayExpression ) {
	            return this;
	        }
            return new ReDimStatement( arrayExpression );
	    }
	}
}
