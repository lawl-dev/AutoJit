using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ExitloopStatement : StatementBase
    {
        public int Level { get; private set; }

        public ExitloopStatement( int level ) {
            Level = level;
        }

        public override string ToSource() {
            return string.Format( "ExitLoop {0}", Level );
        }

        public override object Clone() {
            return new ExitloopStatement( Level );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>(); }
        }
    }
}
