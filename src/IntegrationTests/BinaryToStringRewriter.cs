using System;
using System.Linq;
using System.Text;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;

namespace IntegrationTests
{
    public class BinaryToStringRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );

        public override ISyntaxNode VisitCallExpression( CallExpression node ) {
            if (node.IdentifierName.Token.Value.StringValue != "BinaryToString" || !(node.Parameter.Count == 1 && node.Parameter.Single() is StringLiteralExpression))
            {
                return base.VisitCallExpression( node );
            }

            var literalExpression = (StringLiteralExpression)node.Parameter.Single();

            return _syntaxFactory.CreateStringLiteralExpression( BinaryToString( literalExpression.LiteralToken.Token.Value.StringValue ) );
        }

        private string BinaryToString( string binary ) {
            string hex = binary.Substring( 2, binary.Length-2 );
            var raw = new byte[hex.Length / 2];
            for ( int i = 0; i < raw.Length; i++ ) {
                raw[i] = Convert.ToByte( hex.Substring( i * 2, 2 ), 16 );
            }
            return Encoding.UTF8.GetString( raw );
        }
    }
}