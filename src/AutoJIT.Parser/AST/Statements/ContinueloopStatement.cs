using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ContinueLoopStatement : StatementBase
    {
        public ContinueLoopStatement( TokenNode level ) {
            Level = level;
        }

        public TokenNode Level { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitContinueLoopStatement( this );
        }

        public override string ToSource() {
            return string.Format( "Continueloop {0}", Level );
        }

        public override object Clone() {
            return new ContinueLoopStatement( Level );
        }

        public ContinueLoopStatement Update( TokenNode level ) {
            return new ContinueLoopStatement( level );
        }
    }
}
