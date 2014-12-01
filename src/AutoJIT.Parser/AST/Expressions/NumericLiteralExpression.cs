using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NumericLiteralExpression : LiteralExpression
    {
        public NumericLiteralExpression( TokenNode literalToken, List<TokenNode> signOperators ) : base( literalToken ) {
            SignOperators = signOperators;
        }

        public List<TokenNode> SignOperators { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add(LiteralToken);
                nodes.AddRange( SignOperators );
                return nodes;
            }
        }

        public bool Negativ {
            get { return SignOperators != null && SignOperators.Count( x => x.Token.Type == TokenType.Minus ) % 2 != 0; }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitNumericLiteralExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitNumericLiteralExpression( this );
        }

        public override string ToSource() {
            string toReturn = "";
            if ( Negativ ) {
                toReturn += "-";
            }
            toReturn += LiteralToken.Token;
            return toReturn;
        }

        public override object Clone() {
            var expression = new NumericLiteralExpression( (TokenNode) LiteralToken.Clone(), SignOperators.Select( x=>(TokenNode)x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }

        public NumericLiteralExpression Update( TokenNode literalToken, List<TokenNode> signOperators ) {
            if ( LiteralToken == literalToken &&
                 EnumerableEquals(SignOperators, signOperators) ) {
                return this;
            }
            var expression = new NumericLiteralExpression( (TokenNode) literalToken.Clone(), signOperators.Select( x=>(TokenNode)x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }
    }
}
