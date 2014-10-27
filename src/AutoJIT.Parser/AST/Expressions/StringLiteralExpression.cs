using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class StringLiteralExpression : LiteralExpression
    {
        public StringLiteralExpression( Token literalToken ) : base( literalToken ) {}

        public override IEnumerable<ISyntaxNode> Children {
            get {
                return new List<IExpressionNode>();
            }
        }

        public override string ToSource() {
            return string.Format( "'{0}'", LiteralToken );
        }

        public override object Clone() {
            return new StringLiteralExpression( LiteralToken );
        }
    }
}
