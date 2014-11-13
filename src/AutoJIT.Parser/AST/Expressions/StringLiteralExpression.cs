using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class StringLiteralExpression : LiteralExpression
    {
        public StringLiteralExpression( TokenNode literalToken ) : base( literalToken ) {}

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitStringLiteralExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}", LiteralToken.ToSource() );
        }

        public override object Clone() {
            return new StringLiteralExpression( (TokenNode) LiteralToken.Clone() );
        }

        public StringLiteralExpression Update( TokenNode literalToken ) {
            if ( LiteralToken == literalToken ) {
                return this;
            }
            return new StringLiteralExpression( (TokenNode) literalToken.Clone() );
        }
    }
}
