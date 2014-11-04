using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class StaticDeclarationStatement : LocalDeclarationStatement
	{
		public StaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression ) : base( variableExpression, initExpression, false ) {}

		public override object Clone() {
			return new StaticDeclarationStatement( (VariableExpression)VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression ) );
		}

        public new StaticDeclarationStatement Update(VariableExpression variableExpression, IExpressionNode initExpression, bool isConst)
        {
	        if ( VariableExpression == variableExpression &&
	             InitExpression == initExpression ) {
	            return this;
	        }

            return new StaticDeclarationStatement( variableExpression, initExpression );
	    }
	}
}
