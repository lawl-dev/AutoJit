using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class DoUntilStatement : StatementBase
    {
        public IExpressionNode Condition { get; private set; }
        public IEnumerable<IStatementNode> Block { get; private set; }

        public DoUntilStatement( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
            Condition = condition;
            Block = block;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Empty;
            toReturn += string.Format( "Do{0}", Environment.NewLine );
            foreach (var node in Block) {
                toReturn += string.Format( "{0}{1}", node.ToSource(), Environment.NewLine );
            }
            toReturn += string.Format( "Until {0}{1}", Condition.ToSource(), Environment.NewLine );
            return toReturn;
        }

        public override object Clone() {
            return new DoUntilStatement( (IExpressionNode) Condition.Clone(), Block.Select( x => (IStatementNode) x.Clone() ) );
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var syntaxNodes = new List<ISyntaxNode>() { Condition };

                if ( Block != null ) {
                    syntaxNodes.AddRange( Block );
                }

                return syntaxNodes;
            }
        }
    }
}
