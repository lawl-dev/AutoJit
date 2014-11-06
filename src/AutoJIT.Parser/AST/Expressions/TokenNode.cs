using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class TokenNode : SyntaxNodeBase
    {
        public TokenNode( Token token ) {
            Token = token;
        }

        public Token Token { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override string ToSource() {
            return Token.ToString();
        }

        public override object Clone() {
            return new TokenNode( new Token { Col = Token.Col, Line = Token.Line, Type = Token.Type, Value = Token.Value } );
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitToken( this );
        }
    }
}
