using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
	public abstract class ExpressionParseBase : ParserBase
	{
		protected static TokenCollection GetInnerExpression( TokenQueue block ) {
			return ParseInner( block, TokenType.Leftparen, TokenType.Rightparen );
		}

		protected static IEnumerable<TokenCollection> GetArrayIndexExpressionTrees( TokenQueue block ) {
			var list = new List<TokenCollection>();

			while( block.Any()
				   && block.Peek().Type == TokenType.Leftsubscript ) {
				TokenCollection arrayIndexExpressionTree = ParseInner( block, TokenType.Leftsubscript, TokenType.Rightsubscript );
				list.Add( new TokenCollection( arrayIndexExpressionTree ) );
			}
			return list;
		}
	}
}
