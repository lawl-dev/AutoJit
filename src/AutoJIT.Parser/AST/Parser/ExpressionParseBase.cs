using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    public abstract class ExpressionParseBase : ParserBase
    {
        protected TokenCollection GetInnerExpression( TokenQueue block ) {
            return ParseInner( block, TokenType.Leftparen, TokenType.Rightparen );
        }

        protected IEnumerable<TokenCollection> GetArrayIndexExpressionTrees( TokenQueue block ) {
            var list = new List<TokenCollection>();

            while ( block.Any() &&
                    block.Peek().Type == TokenType.Leftsubscript ) {
                var arrayIndexExpressionTree = ParseInner( block, TokenType.Leftsubscript, TokenType.Rightsubscript );
                list.Add( new TokenCollection( arrayIndexExpressionTree ) );
            }
            return list;
        }
    }
}
