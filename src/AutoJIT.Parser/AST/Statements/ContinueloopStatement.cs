using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ContinueloopStatement : StatementBase
    {
        public ContinueloopStatement( int level ) {
            Level = level;
        }

        public int Level {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode>();
            }
        }

        public override string ToSource() {
            return string.Format( "Continueloop {0}", Level );
        }

        public override object Clone() {
            return new ContinueloopStatement( Level );
        }
    }
}
