using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class WhileStatement : StatementBase
    {
        public WhileStatement( IExpressionNode condition, BlockStatement block ) {
            Condition = condition;
            Block = block;
            Initialize();
        }

        public IExpressionNode Condition { get; private set; }
        public BlockStatement Block { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    Condition
                };
                syntaxNodes.Add( Block );
                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitWhileStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "While {0}", Condition.ToSource() );
            toReturn += Environment.NewLine;
            toReturn += Block.ToSource();
            toReturn += "WEnd";
            return toReturn;
        }

        public override object Clone() {
            return new WhileStatement( (IExpressionNode) Condition.Clone(), (BlockStatement) Block.Clone() );
        }

        public WhileStatement Update( IExpressionNode condition, BlockStatement block ) {
            if ( Condition == condition &&
                 Block == block ) {
                return this;
            }
            return new WhileStatement( (IExpressionNode) condition.Clone(), (BlockStatement) block.Clone() );
        }
    }
}
