using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class FalseLiteralExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitFalseLiteralExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitFalseLiteralExpression( this );
        }

        public override string ToSource() {
            return "False";
        }

        public override object Clone() {
            return new FalseLiteralExpression();
        }

        public FalseLiteralExpression Update() {
            return new FalseLiteralExpression();
        }
    }
}
