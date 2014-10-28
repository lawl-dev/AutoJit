using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class ArrayInitExpression : ExpressionBase
    {
        public ArrayInitExpression( List<IExpressionNode> toAssign ) {
            ToAssign = toAssign;
            Initialize();
        }

        public List<IExpressionNode> ToAssign { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode>( ToAssign );
            }
        }

        public override string ToSource() {
            return string.Format( "[{0}]", string.Join( ", ", ToAssign.Select( x => x.ToSource() ) ) );
        }

        public override object Clone() {
            return new ArrayInitExpression( ToAssign.Select( x => (IExpressionNode)x.Clone() ).ToList() );
        }
    }
}
