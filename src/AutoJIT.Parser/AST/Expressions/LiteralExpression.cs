using System.Collections.Generic;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class LiteralExpression : ExpressionBase
    {
        protected LiteralExpression( TokenNode literalToken ) {
            LiteralToken = literalToken;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return LiteralToken.ToEnumerable(); }
        }

        public TokenNode LiteralToken { get; private set; }
    }
}
