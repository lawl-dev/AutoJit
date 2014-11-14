using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NegateExpression : ExpressionBase
    {
        public NegateExpression( IExpressionNode expressionNode ) {
            ExpressionNode = expressionNode;
        }

        public IExpressionNode ExpressionNode { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return ExpressionNode.ToEnumerable(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitNegateExpression( this );
        }

        public override string ToSource() {
            return string.Format( "-{0}", ExpressionNode.ToSource() );
        }

        public override object Clone() {
            var expression = new NegateExpression( (IExpressionNode) ExpressionNode.Clone() );
            expression.Initialize();
            return expression;
        }

        public NegateExpression Update( IExpressionNode expressionNode ) {
            if ( ExpressionNode == expressionNode ) {
                return this;
            }
            var expression = new NegateExpression( (IExpressionNode) expressionNode.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
