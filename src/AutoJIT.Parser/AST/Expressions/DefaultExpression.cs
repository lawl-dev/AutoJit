using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class DefaultExpression : ExpressionBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>(); }
        }

        public override string ToSource() {
            return "Default";
        }

        public override object Clone() {
            return new DefaultExpression();
        }
    }
}
