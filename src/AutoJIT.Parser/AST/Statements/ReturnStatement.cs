using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ReturnStatement : StatementBase
    {
        public ReturnStatement( IExpressionNode returnExpression ) {
            ReturnExpression = returnExpression;
        }

        public IExpressionNode ReturnExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    ReturnExpression
                };
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitReturnStatement(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitReturnStatement( this );
        }

        public override string ToSource() {
            return string.Format( "Return {0}", ReturnExpression.ToSource() );
        }

        public override object Clone() {
            var statement = new ReturnStatement( CloneAs<IExpressionNode>( ReturnExpression ) );
            statement.Initialize();
            return statement;
        }

        public ReturnStatement Update( IExpressionNode returnExpression ) {
            if ( ReturnExpression == returnExpression ) {
                return this;
            }
            var statement = new ReturnStatement( (IExpressionNode) returnExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
