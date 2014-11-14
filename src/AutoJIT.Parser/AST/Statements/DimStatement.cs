using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class DimStatement : StatementBase
    {
        public DimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            VariableExpression = variableExpression;
            InitExpression = initExpression;
        }

        public VariableExpression VariableExpression { get; private set; }
        public IExpressionNode InitExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    VariableExpression,
                    InitExpression
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitDimStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "Dim {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public override object Clone() {
            var statement = new DimStatement( (VariableExpression) VariableExpression.Clone(), CloneAs<IExpressionNode>( InitExpression ) );
            statement.Initialize();
            return statement;
        }

        public DimStatement Update( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( VariableExpression == variableExpression &&
                 InitExpression == initExpression ) {
                return this;
            }
            var statement = new DimStatement( (VariableExpression) variableExpression.Clone(), (IExpressionNode) initExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
