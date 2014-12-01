using System;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class GlobalEnumDeclarationStatement : EnumDeclarationStatement
    {
        public GlobalEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) : base( variableExpression, userInitExpression, autoInitExpression ) {}

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitGlobalEnumDeclarationStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitGlobalEnumDeclarationStatement( this );
        }

        public override object Clone() {
            var statement = new GlobalEnumDeclarationStatement( (VariableExpression) VariableExpression.Clone(), (IExpressionNode) UserInitExpression.Clone(), (IExpressionNode) AutoInitExpression.Clone() );
            statement.Initialize();
            return statement;
        }

        public override string ToSource() {
            throw new NotImplementedException();
        }


        public GlobalEnumDeclarationStatement Update( VariableExpression variableExpression, IExpressionNode userInitExpression, IExpressionNode autoInitExpression ) {
            if ( VariableExpression == variableExpression &&
                 UserInitExpression == userInitExpression &&
                 AutoInitExpression == autoInitExpression ) {
                return this;
            }
            var statement = new GlobalEnumDeclarationStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) userInitExpression.Clone(), (IExpressionNode) autoInitExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
