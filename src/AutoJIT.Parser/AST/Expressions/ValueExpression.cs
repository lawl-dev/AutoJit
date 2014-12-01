using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class ValueExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override string ToSource() {
            return "value";
        }

        public override object Clone() {
            return new ValueExpression();
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitValueExpression( this );
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitValueExpression(this);
        }

        public ValueExpression Update() {
            return new ValueExpression();
        }
    }
}