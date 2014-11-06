using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ContinueCaseStatement : StatementBase
    {
        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitContinueCaseStatement( this );
        }

        public override string ToSource() {
            return "ContinueCase";
        }

        public override object Clone() {
            return new ContinueCaseStatement();
        }

        public ContinueCaseStatement Update() {
            return new ContinueCaseStatement();
        }
    }
}
