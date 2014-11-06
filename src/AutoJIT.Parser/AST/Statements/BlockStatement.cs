using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class BlockStatement : StatementBase
    {
        public BlockStatement( IEnumerable<IStatementNode> block ) {
            Block = block;
        }

        public IEnumerable<IStatementNode> Block { get; set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var toReturn = new List<ISyntaxNode>();
                toReturn.AddRange( Block );
                return toReturn;
            }
        }

        public override string ToSource() {
            return string.Join( Environment.NewLine, Block.Select( x => x.ToSource() ) );
        }

        public override object Clone() {
            return new BlockStatement( Block.Select( x => (IStatementNode) x.Clone() ) );
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitBlockStatement( this );
        }

        public BlockStatement Update( IEnumerable<IStatementNode> block ) {
            throw new NotImplementedException();
        }
    }
}
