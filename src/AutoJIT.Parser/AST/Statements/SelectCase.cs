using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class SelectCase : StatementBase
    {
        public SelectCase( IExpressionNode condition, BlockStatement block ) {
            Condition = condition;
            Block = block;
            Initialize();
        }

        public IExpressionNode Condition { get; set; }
        public BlockStatement Block { get; set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();

                nodes.Add( Condition );
                nodes.Add( Block );

                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitSelectCase( this );
        }

        public override string ToSource() {
            string toReturn = string.Empty;
            toReturn += string.Format( "Case {0}{1}", Condition.ToSource(), Environment.NewLine );
            toReturn += Block.ToSource();
            return toReturn;
        }

        public override object Clone() {
            return new SelectCase( (IExpressionNode) Condition.Clone(), (BlockStatement) Block.Clone() );
        }

        public SelectCase Update( IExpressionNode condition, BlockStatement block ) {
            if ( Condition == condition &&
                 Block == block ) {
                return this;
            }
            return new SelectCase( condition, block );
        }
    }
}
