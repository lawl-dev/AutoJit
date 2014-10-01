using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class LiteralExpression : ExpressionBase
    {
        public readonly Token LiteralToken;

        protected LiteralExpression( Token literalToken ) {
            LiteralToken = literalToken;
        }
    }
}
