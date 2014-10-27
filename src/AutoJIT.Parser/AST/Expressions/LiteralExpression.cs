using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class LiteralExpression : ExpressionBase
    {
        protected LiteralExpression( Token literalToken ) {
            LiteralToken = literalToken;
        }

        public Token LiteralToken {
            get;
            private set;
        }
    }
}
