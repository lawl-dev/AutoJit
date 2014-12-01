using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class TrueLiteralExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitTrueLiteralExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitTrueLiteralExpression( this );
        }

        public override string ToSource() {
            return "True";
        }

        public override object Clone() {
            return new TrueLiteralExpression();
        }

        public TrueLiteralExpression Update() {
            return new TrueLiteralExpression();
        }
    }
}
