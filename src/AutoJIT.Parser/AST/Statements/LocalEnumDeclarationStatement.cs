using System.Collections.Generic;
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

        public override string ToSource() {
            throw new System.NotImplementedException();
        }

        public override object Clone() {
            var statement = new LocalEnumDeclarationStatement( (VariableExpression) VariableExpression.Clone(), (IExpressionNode) UserInitExpression.Clone(), (IExpressionNode) AutoInitExpression.Clone() );
            statement.Initialize();
            return statement;
        }

        public LocalEnumDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) {
            if ( VariableExpression == variableExpression &&
                 UserInitExpression == userInitExpression &&
                 AutoInitExpression == autoInitExpression ) {
                return this;
            }
            var statement = new LocalEnumDeclarationStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) userInitExpression.Clone(), (IExpressionNode) autoInitExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
