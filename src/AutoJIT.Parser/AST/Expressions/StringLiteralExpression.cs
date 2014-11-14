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
            var expression = new StringLiteralExpression( (TokenNode) LiteralToken.Clone() );
            expression.Initialize();
            return expression;
        }

        public StringLiteralExpression Update( TokenNode literalToken ) {
            if ( LiteralToken == literalToken ) {
                return this;
            }
            var expression = new StringLiteralExpression( (TokenNode) literalToken.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
