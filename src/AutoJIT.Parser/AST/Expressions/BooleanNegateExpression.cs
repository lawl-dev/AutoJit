using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class BooleanNegateExpression : ExpressionBase
    {
        public BooleanNegateExpression( IExpressionNode left, Token @operator ) {
            Left = left;
            Operator = @operator;
            Initialize();
        }

        public IExpressionNode Left { get; private set; }
        public Token Operator { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Left.ToEnumerable(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitBooleanNegateExpression( this );
        }

        public override string ToSource() {
            return string.Format( "NOT {0}", Left.ToSource() );
        }

        public override object Clone() {
            return new BooleanNegateExpression( (IExpressionNode) Left.Clone(), Operator );
        }

        public BooleanNegateExpression Update( IExpressionNode left, Token @operator ) {
            if ( Left == left &&
                 Operator == @operator ) {
                return this;
            }
            return new BooleanNegateExpression( left, @operator );
        }
    }
}
