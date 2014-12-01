using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class ArrayInitExpression : ExpressionBase
    {
        public ArrayInitExpression( List<IExpressionNode> toAssign ) {
            ToAssign = toAssign;
        }

        public List<IExpressionNode> ToAssign { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>( ToAssign ); }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitArrayInitExpression( this );
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitArrayInitExpression( this );
        }

        public override string ToSource() {
            return string.Format( "[{0}]", string.Join( ", ", ToAssign.Select( x => x.ToSource() ) ) );
        }

        public override object Clone() {
            var initExpression = new ArrayInitExpression( ToAssign.Select( x => (IExpressionNode) x.Clone() ).ToList() );
            initExpression.Initialize();
            return initExpression;
        }

        public ArrayInitExpression Update( List<IExpressionNode> toAssign ) {
            if ( EnumerableEquals(ToAssign, toAssign) ) {
                return this;
            }
            var initExpression = new ArrayInitExpression( toAssign.Select( x=>(IExpressionNode)x.Clone() ).ToList() );
            initExpression.Initialize();
            return initExpression;
        }
    }
}
