using System.Collections.Generic;
using System.Linq;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NullExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get {
                return Enumerable.Empty<ISyntaxNode>();
            }
        }

        public override string ToSource() {
            return "Null";
        }

        public override object Clone() {
            return new NullExpression();
        }
    }
}
