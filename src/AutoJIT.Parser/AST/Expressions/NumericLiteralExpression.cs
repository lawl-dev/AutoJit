using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NumericLiteralExpression : LiteralExpression
    {
        public NumericLiteralExpression( Token literalToken, IEnumerable<Token> signOperators ) : base( literalToken ) {
            SignOperators = signOperators;
        }

        public IEnumerable<Token> SignOperators { get; private set; }

        public bool Negativ {
            get { return SignOperators != null && SignOperators.Count( x => x.Type == TokenType.Minus ) % 2 != 0; }
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitNumericLiteralExpression( this );
        }

        public override string ToSource() {
            string toReturn = "";
            if ( Negativ ) {
                toReturn += "-";
            }
            toReturn += LiteralToken;
            return toReturn;
        }

        public override object Clone() {
            return new NumericLiteralExpression( LiteralToken, SignOperators );
        }

        public NumericLiteralExpression Update( Token literalToken, IEnumerable<Token> signOperators ) {
            if ( LiteralToken == literalToken &&
                 SignOperators == signOperators ) {
                return this;
            }
            return new NumericLiteralExpression( literalToken, signOperators );
        }
    }
}
