using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class IfElseStatement : StatementBase
    {
        public IfElseStatement( IExpressionNode condition, BlockStatement ifBlock, IEnumerable<IExpressionNode> elseIfConditions, IEnumerable<BlockStatement> elseIfBlocks, BlockStatement elseBlock ) {
            Condition = condition;
            IfBlock = ifBlock;
            ElseIfConditions = elseIfConditions;
            ElseIfBlocks = elseIfBlocks;
            ElseBlock = elseBlock;
            Initialize();
        }

        public IExpressionNode Condition { get; private set; }
        public BlockStatement IfBlock { get; private set; }
        public IEnumerable<IExpressionNode> ElseIfConditions { get; private set; }
        public IEnumerable<BlockStatement> ElseIfBlocks { get; private set; }
        public BlockStatement ElseBlock { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode>();
                syntaxNodes.Add( Condition );

                if ( IfBlock != null ) {
                    syntaxNodes.Add( IfBlock );
                }

                if ( ElseIfConditions != null ) {
                    syntaxNodes.AddRange( ElseIfConditions );
                }

                if ( ElseIfBlocks != null ) {
                    syntaxNodes.AddRange( ElseIfBlocks );
                }

                if ( ElseBlock != null ) {
                    syntaxNodes.Add( ElseBlock );
                }

                return syntaxNodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitIfElseStatement( this );
        }

        public override string ToSource() {
            string toReturn = string.Format( "If {0} Then{1}", Condition.ToSource(), Environment.NewLine );
            toReturn += IfBlock.ToSource();

            if ( ElseIfConditions != null ) {
                for ( int i = 0; i < ElseIfConditions.Count(); i++ ) {
                    IExpressionNode conditionExpression = ElseIfConditions.Skip( i ).First();
                    BlockStatement statements = ElseIfBlocks.Skip( i ).First();
                    toReturn += string.Format( "ElseIf {0}{1}", conditionExpression.ToSource(), Environment.NewLine );
                    toReturn += statements.ToSource();
                }
            }

            if ( ElseBlock != null && ElseBlock.Block.Any()) {
                toReturn += "Else"+Environment.NewLine;
                toReturn += ElseBlock.ToSource();
            }
            toReturn += "EndIf";
            return toReturn;
        }

        public override object Clone() {
            return new IfElseStatement(
                (IExpressionNode) Condition.Clone(),
                (BlockStatement) IfBlock.Clone(),
                CloneEnumerableAs<IExpressionNode>( ElseIfConditions ),
                ElseIfBlocks != null
                    ? ElseIfBlocks.Select( x => (BlockStatement) x.Clone() )
                    : null,
                (BlockStatement) ElseBlock.Clone() );
        }

        public IfElseStatement Update( IExpressionNode condition, BlockStatement ifBlock, IEnumerable<IExpressionNode> elseIfConditions, IEnumerable<BlockStatement> elseIfBlocks, BlockStatement elseBlock ) {
            if ( Condition == condition &&
                 IfBlock == ifBlock &&
                 EnumerableEquals(ElseIfConditions, elseIfConditions ) &&
                 EnumerableEquals( ElseIfBlocks, elseIfBlocks ) &&
                 ElseBlock == elseBlock ) {
                return this;
            }
            return new IfElseStatement( (IExpressionNode) condition.Clone(), (BlockStatement) ifBlock.Clone(), elseIfConditions.Select( x=>(IExpressionNode)x.Clone() ), elseIfBlocks.Select( x=>(BlockStatement)x.Clone() ), (BlockStatement) elseBlock.Clone() );
        }
    }
}
