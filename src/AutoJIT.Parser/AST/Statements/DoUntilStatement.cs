using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class DoUntilStatement : StatementBase
    {
        public DoUntilStatement( IExpressionNode condition, BlockStatement block ) {
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

                if ( Block != null ) {
                    syntaxNodes.Add( Block );
                }

                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitDoUntilStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Empty;
            toReturn += string.Format( "Do{0}", Environment.NewLine );
            toReturn += Block.ToSource();
            toReturn += string.Format( "Until {0}", Condition.ToSource() );
            return toReturn;
        }

        public override object Clone() {
            return new DoUntilStatement( (IExpressionNode) Condition.Clone(), (BlockStatement) Block.Clone() );
        }

        public DoUntilStatement Update( IExpressionNode condition, BlockStatement block ) {
            if ( Condition == condition &&
                 Block == block ) {
                return this;
            }
            return new DoUntilStatement( (IExpressionNode) condition.Clone(), (BlockStatement) block.Clone() );
        }
    }
}
