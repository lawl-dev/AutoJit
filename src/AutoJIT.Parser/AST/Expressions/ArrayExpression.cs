using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class ArrayExpression : VariableExpression
	{
		public ArrayExpression( string identifierName, IEnumerable<IExpressionNode> accessParameter ) : base( identifierName ) {
			AccessParameter = accessParameter;
			Initialize();
		}

		public IEnumerable<IExpressionNode> AccessParameter { get; private set; }

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return AccessParameter;
			}
		}

		public override string ToSource() {
			return string.Format( "${0}{1}", IdentifierName, AccessParameter.Select( x => string.Format( "[{0}]", x.ToSource() ) ) );
		}

		public override object Clone() {
			return new ArrayExpression( (string)IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( AccessParameter ) );
		}
	}
}
