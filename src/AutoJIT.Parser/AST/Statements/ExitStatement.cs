using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ExitStatement : StatementBase
    {
        public ExitStatement( IExpressionNode exitExpression ) {
            ExitExpression = exitExpression;
        }

        public IExpressionNode ExitExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    ExitExpression
                };
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitExitStatement( this );
        }

        public override string ToSource() {
            return string.Format( "Exit {0}", ExitExpression.ToSource() );
        }

        public override object Clone() {
            var statement = new ExitStatement( (IExpressionNode) ExitExpression.Clone() );
            statement.Initialize();
            return statement;
        }

        public ExitStatement Update( IExpressionNode exitExpression ) {
            if ( ExitExpression == exitExpression ) {
                return this;
            }
            var statement = new ExitStatement( (IExpressionNode) exitExpression.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
