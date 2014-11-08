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
            Initialize();
        }

        public List<IExpressionNode> ToAssign { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>( ToAssign ); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitArrayInitExpression( this );
        }

        public override string ToSource() {
            return string.Format( "[{0}]", string.Join( ", ", ToAssign.Select( x => x.ToSource() ) ) );
        }

        public override object Clone() {
            return new ArrayInitExpression( ToAssign.Select( x => (IExpressionNode) x.Clone() ).ToList() );
        }

        public ArrayInitExpression Update( IEnumerable<IExpressionNode> toAssign ) {
            if ( EnumerableEquals(ToAssign, toAssign) ) {
                return this;
            }
            return new ArrayInitExpression( ToAssign.Select( x=>(IExpressionNode)x.Clone() ).ToList() );
        }
    }
}
