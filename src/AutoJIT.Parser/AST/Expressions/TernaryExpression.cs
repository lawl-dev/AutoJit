using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class TernaryExpression : ExpressionBase
    {
        public TernaryExpression( IExpressionNode condition, IExpressionNode ifTrue, IExpressionNode ifFalse ) {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public IExpressionNode Condition { get; private set; }
        public IExpressionNode IfTrue { get; private set; }
        public IExpressionNode IfFalse { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<IExpressionNode> {
                    Condition,
                    IfTrue,
                    IfFalse
                };
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitTernaryExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitTernaryExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0} ? {1} : {2}", Condition.ToSource(), IfTrue.ToSource(), IfFalse.ToSource() );
        }

        public override object Clone() {
            var expression = new TernaryExpression( (IExpressionNode) Condition.Clone(), (IExpressionNode) IfTrue.Clone(), (IExpressionNode) IfFalse.Clone() );
            expression.Initialize();
            return expression;
        }

        public TernaryExpression Update( IExpressionNode condition, IExpressionNode ifTrue, IExpressionNode ifFalse ) {
            if ( Condition == condition &&
                 IfTrue == ifTrue &&
                 IfFalse == ifFalse ) {
                return this;
            }
            var expression = new TernaryExpression( (IExpressionNode) condition.Clone(), (IExpressionNode) ifTrue.Clone(), (IExpressionNode) ifFalse.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
