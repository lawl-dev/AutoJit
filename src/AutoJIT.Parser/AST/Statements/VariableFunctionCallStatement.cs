using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class VariableFunctionCallStatement : StatementBase
	{
		public VariableFunctionCallExpression VariableFunctionCallExpression { get; private set; }

		public VariableFunctionCallStatement(VariableFunctionCallExpression variableFunctionCallExpression) {
			VariableFunctionCallExpression = variableFunctionCallExpression;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var nodes = new List<ISyntaxNode>();
				nodes.Add( VariableFunctionCallExpression );
				return nodes;
			}
		}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitVariableFunctionCallStatement( this );
	    }

	    public override string ToSource() {
			return VariableFunctionCallExpression.ToSource();
		}

		public override object Clone() {
			return new VariableFunctionCallStatement( (VariableFunctionCallExpression)VariableFunctionCallExpression.Clone());
		}
	}
}