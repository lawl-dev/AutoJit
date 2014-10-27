using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ReDimStatement : StatementBase
    {
        public ReDimStatement( ArrayExpression arrayExpression ) {
            ArrayExpression = arrayExpression;
            Initialize();
        }

        public ArrayExpression ArrayExpression {
            get;
            private set;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<ISyntaxNode> {
                    ArrayExpression
                };
            }
        }

        public override string ToSource() {
            return string.Format( "Redim {0}", ArrayExpression.ToSource() );
        }

        public override object Clone() {
            return new ReDimStatement( (ArrayExpression)ArrayExpression.Clone() );
        }
    }
}
