using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
	public sealed class UserfunctionCallExpression : CallExpression
	{
		public UserfunctionCallExpression( string identifierName, IEnumerable<IExpressionNode> parameter ) : base( identifierName, parameter ) {}

		public override object Clone() {
			return new UserfunctionCallExpression( (string)IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( Parameter ) );
		}

	    public new UserfunctionCallExpression Update( IEnumerable<IExpressionNode> parameter, string identifierName ) {
	        if ( IdentifierName == identifierName &&
	             Parameter == parameter ) {
	            return this;
	        }
            return new UserfunctionCallExpression( identifierName,parameter );
	    }
	}
}
