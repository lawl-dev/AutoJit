using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class LiteralExpression : ExpressionBase
    {
        public Token LiteralToken { get; private set; }

        protected LiteralExpression( Token literalToken ) {
            LiteralToken = literalToken;
        }
    }
}
