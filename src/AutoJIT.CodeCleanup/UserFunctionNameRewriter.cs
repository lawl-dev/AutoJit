using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.CodeCleanup
{
    public class UserFunctionNameRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private readonly ITokenFactory _tokenFactory = new TokenFactory();

        public override ISyntaxNode VisitToken( TokenNode node ) {
            if ( !( node.Parent is UserfunctionCallExpression || node.Parent is UserfunctionExpression || node.Parent is Function ) ) {
                return base.VisitToken( node );
            }

            if ( node.Token.Value.StringValue.StartsWith( "_" ) ) {
                return base.VisitToken( node );
            }

            return _syntaxFactory.CreateTokenNode( _tokenFactory.CreateUserfunction( "_"+node.Token.Value.StringValue, node.Token.Col, node.Token.Line ) );
        }
    }
}