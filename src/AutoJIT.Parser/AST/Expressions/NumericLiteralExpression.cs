using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
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
            get { return new List<IExpressionNode>(); }
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
    }
}
