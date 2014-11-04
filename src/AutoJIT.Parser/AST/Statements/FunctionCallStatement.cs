using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class FunctionCallStatement : StatementBase
	{
		public FunctionCallStatement( IExpressionNode functionCallExpression ) {
			FunctionCallExpression = functionCallExpression;
			Initialize();
		}

		public IExpressionNode FunctionCallExpression { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return new List<ISyntaxNode> {
					FunctionCallExpression
				};
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitFunctionCallStatement( this );
	    }

	    public override string ToSource() {
			return FunctionCallExpression.ToSource();
		}

		public override object Clone() {
			return new FunctionCallStatement( (IExpressionNode)FunctionCallExpression.Clone() );
		}

	    public FunctionCallStatement Update( IExpressionNode functionCallExpression ) {
	        if ( FunctionCallExpression == functionCallExpression ) {
	            return this;
	        }

            return new FunctionCallStatement( functionCallExpression );
	    }
	}
}
