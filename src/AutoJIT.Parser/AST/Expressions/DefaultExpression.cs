using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class DefaultExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitDefaultExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitDefaultExpression( this );
        }

        public override string ToSource() {
            return "Default";
        }

        public override object Clone() {
            return new DefaultExpression();
        }

        public DefaultExpression Update() {
            return new DefaultExpression();
        }
    }
}
