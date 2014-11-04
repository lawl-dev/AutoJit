using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
	public sealed class GlobalEnumDeclarationStatement : EnumDeclarationStatement
	{
		public GlobalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) : base( variableExpression, userInitExpression, autoInitExpression ) {}

	    public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
	        return visitor.VisitGlobalEnumDeclarationStatement( this );
	    }

	    public override object Clone() {
			return new GlobalEnumDeclarationStatement( (VariableExpression)VariableExpression.Clone(), (IExpressionNode)UserInitExpression.Clone(), (IExpressionNode)AutoInitExpression.Clone() );
		}

	    public GlobalEnumDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) {
	        if ( VariableExpression == variableExpression &&
	             UserInitExpression == userInitExpression &&
	             AutoInitExpression == autoInitExpression ) {
	            return this;
	        }
            return new GlobalEnumDeclarationStatement( variableExpression, userInitExpression, autoInitExpression );
	    }
	}
}
