using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NumericLiteralExpression : LiteralExpression
    {
        public NumericLiteralExpression( TokenNode literalToken, IEnumerable<TokenNode> signOperators ) : base( literalToken ) {
            SignOperators = signOperators;
        }

        public IEnumerable<TokenNode> SignOperators { get; private set; }

        public bool Negativ {
            get { return SignOperators != null && SignOperators.Count( x => x.Token.Type == TokenType.Minus ) % 2 != 0; }
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

        public NumericLiteralExpression Update( TokenNode literalToken, IEnumerable<TokenNode> signOperators ) {
            if ( LiteralToken == literalToken &&
                 EnumerableEquals(SignOperators, signOperators) ) {
                return this;
            }
            return new NumericLiteralExpression( (TokenNode) literalToken.Clone(), signOperators.Select( x=>(TokenNode)x.Clone() ) );
        }
    }
}
