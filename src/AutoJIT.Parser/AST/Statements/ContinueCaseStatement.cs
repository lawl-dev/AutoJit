using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ContinueCaseStatement : StatementBase
    {
        public override string ToSource() {
            return "ContinueCase";
        }

        public override object Clone() {
            return new ContinueCaseStatement();
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>(); }
        }
    }
}
