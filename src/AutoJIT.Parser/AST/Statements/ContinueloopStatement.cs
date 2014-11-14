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
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( Level );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitContinueLoopStatement( this );
        }

        public override string ToSource() {
            return string.Format(
                "Continueloop {0}", Level != null
                    ? Level.Token.Value.Int32Value
                    : 1 );
        }

        public override object Clone() {
            var statement = new ContinueLoopStatement( (TokenNode) Level.Clone() );
            statement.Initialize();
            return statement;
        }

        public ContinueLoopStatement Update( TokenNode level ) {
            if ( Level == level ) {
                return this;
            }
            var statement = new ContinueLoopStatement( (TokenNode) level.Clone() );
            statement.Initialize();
            return statement;
        }
    }
}
