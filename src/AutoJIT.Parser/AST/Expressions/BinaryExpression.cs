using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class BinaryExpression : ExpressionBase
    {
        public BinaryExpression( IExpressionNode left, IExpressionNode right, TokenNode @operator ) {
            Operator = @operator;
            Right = right;
            Left = left;
        }

        public IExpressionNode Left { get; private set; }

        public IExpressionNode Right { get; private set; }

        public TokenNode Operator { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var toReturn = new List<ISyntaxNode> {
                    Left,
                    Operator,
                    Right
                };
                return toReturn;
            }
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitBinaryExpression( this );
        }
        
        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitBinaryExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0} {1} {2}", Left.ToSource(), Operator.ToSource(), Right.ToSource() );
        }

        public override object Clone() {
            var expression = new BinaryExpression( (IExpressionNode) Left.Clone(), (IExpressionNode) Right.Clone(), (TokenNode) Operator.Clone() );
            expression.Initialize();
            return expression;
        }

        public BinaryExpression Update( IExpressionNode left, IExpressionNode right, TokenNode @operator ) {
            if ( Left == left &&
                 Right == right &&
                 Operator == @operator ) {
                return this;
            }
            var expression = new BinaryExpression( (IExpressionNode) left.Clone(), (IExpressionNode) right.Clone(), (TokenNode) @operator.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
