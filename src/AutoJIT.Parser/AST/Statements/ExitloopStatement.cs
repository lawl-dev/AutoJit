using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ExitloopStatement : StatementBase
    {
        public ExitloopStatement( TokenNode level ) {
            Level = level;
        }

        public TokenNode Level { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( Level );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitExitloopStatement( this );
        }

        public override string ToSource() {
            return string.Format( "ExitLoop {0}", Level.Token );
        }

        public override object Clone() {
            return new ExitloopStatement( Level );
        }

        public ExitloopStatement Update( TokenNode level ) {
            if ( Level == level ) {
                return this;
            }
            return new ExitloopStatement( (TokenNode) level.Clone() );
        }
    }
}
