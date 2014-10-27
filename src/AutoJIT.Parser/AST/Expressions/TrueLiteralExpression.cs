using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class TrueLiteralExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<IExpressionNode>();
            }
        }

        public override string ToSource() {
            return "True";
        }

        public override object Clone() {
            return new TrueLiteralExpression();
        }
    }
}
