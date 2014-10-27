using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class WhileStatement : StatementBase
    {
        public WhileStatement( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
            Condition = condition;
            Block = block;
            Initialize();
        }

        public IExpressionNode Condition {
            get;
            private set;
        }
        public IEnumerable<IStatementNode> Block {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode> {
                    Condition
                };
                syntaxNodes.AddRange( Block );
                return syntaxNodes;
            }
        }

        public override string ToSource() {
            string toReturn = string.Format( "While {0}", Condition.ToSource() );
            toReturn += Environment.NewLine;
            foreach(IStatementNode statement in Block) {
                toReturn += string.Format( "{0}{1}", statement.ToSource(), Environment.NewLine );
            }
            toReturn += "WEnd";
            return toReturn;
        }

        public override object Clone() {
            return new WhileStatement( (IExpressionNode)Condition.Clone(), Block.Select( x => (IStatementNode)x.Clone() ) );
        }
    }
}
