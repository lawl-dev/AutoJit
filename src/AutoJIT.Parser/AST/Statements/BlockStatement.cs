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
            if ( Block.Count() > 1 ) {
                var statements = string.Join(Environment.NewLine + "	", Block.Select(x => x.ToSource()));

                return string.Format("	{0}{1}", statements, Environment.NewLine);
            }
            if ( Block.Count() == 1 ) {
                return string.Format("	{0}{1}", Block.Single().ToSource(), Environment.NewLine);
            }
            return string.Empty;
        }

        public override object Clone() {
            var statement = new BlockStatement( Block.Select( x => (IStatementNode) x.Clone() ) );
            statement.Initialize();
            return statement;
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitBlockStatement( this );
        }

        public BlockStatement Update( IEnumerable<IStatementNode> block ) {
            if ( EnumerableEquals( Block, block ) ) {
                return this;
            }
            var statement = new BlockStatement( block.Select( x=>(IStatementNode)x.Clone() ) );
            statement.Initialize();
            return statement;
        }
    }
}
