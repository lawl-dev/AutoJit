using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ContinueloopStatement : StatementBase
    {
        public readonly int Level;

        public ContinueloopStatement( int level ) {
            Level = level;
        }

        public override string ToSource() {
            return string.Format( "Continueloop {0}", Level );
        }

        public override object Clone() {
            return new ContinueloopStatement( Level );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>(); }
        }
    }
}
