using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

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

		public override string ToSource() {
			return VariableFunctionCallExpression.ToSource();
		}

		public override object Clone() {
			return new VariableFunctionCallStatement( (VariableFunctionCallExpression)VariableFunctionCallExpression.Clone());
		}
	}
}