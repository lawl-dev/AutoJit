using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ExitStatement : StatementBase
    {
        public ExitStatement( IExpressionNode expressionNode ) {
            ExpressionNode = expressionNode;
            Initialize();
        }

        public IExpressionNode ExpressionNode {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    ExpressionNode
                };
            }
        }

        public override string ToSource() {
            return string.Format( "Exit {0}", ExpressionNode.ToSource() );
        }

        public override object Clone() {
            return new ExitStatement( (IExpressionNode)ExpressionNode.Clone() );
        }
    }
}
