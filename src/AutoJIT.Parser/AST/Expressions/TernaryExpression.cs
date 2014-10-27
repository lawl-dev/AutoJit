using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public class TernaryExpression : ExpressionBase
    {
        public TernaryExpression( IExpressionNode condition, IExpressionNode ifTrue, IExpressionNode ifFalse ) {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
            Initialize();
        }

        public IExpressionNode Condition {
            get;
            private set;
        }
        public IExpressionNode IfTrue {
            get;
            private set;
        }
        public IExpressionNode IfFalse {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<IExpressionNode> {
                    Condition, IfTrue, IfFalse
                };
            }
        }

        public override string ToSource() {
            return string.Format( "{0} ? {1} : {2}", Condition.ToSource(), IfTrue.ToSource(), IfFalse.ToSource() );
        }

        public override object Clone() {
            return new TernaryExpression( (IExpressionNode)Condition.Clone(), (IExpressionNode)IfTrue.Clone(), (IExpressionNode)IfFalse.Clone() );
        }
    }
}
