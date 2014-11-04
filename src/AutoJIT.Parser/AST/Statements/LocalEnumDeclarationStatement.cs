using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public class LocalEnumDeclarationStatement : EnumDeclarationStatement
	{
		public LocalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) : base( variableExpression, userInitExpression, autoInitExpression ) {}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitLocalEnumDeclarationStatement( this );
	    }

	    public override object Clone() {
			return new LocalEnumDeclarationStatement( (VariableExpression)VariableExpression.Clone(), (IExpressionNode)UserInitExpression.Clone(), (IExpressionNode)AutoInitExpression.Clone() );
		}

	    public LocalEnumDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) {
	        if ( VariableExpression == variableExpression &&
	             UserInitExpression == userInitExpression &&
	             AutoInitExpression == autoInitExpression ) {
	            return this;
	        }
            return new LocalEnumDeclarationStatement( variableExpression, userInitExpression, autoInitExpression );
	    }
	}
}
