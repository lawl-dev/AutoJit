using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser
{
	public sealed class OperatorPrecedenceService : IOperatorPrecedenceService
	{
		private readonly ITokenFactory _tokenFactory;

		public OperatorPrecedenceService( ITokenFactory tokenFactory ) {
			_tokenFactory = tokenFactory;
		}

		public TokenCollection PrepareOperatorPrecedence( TokenCollection expression ) {
			TokenCollection tokenList = AddParenthesesForOperator( expression, TokenType.NOT );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.Pow );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.Mult, TokenType.Div );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.Plus, TokenType.Minus );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.Concat );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual, TokenType.Equal, TokenType.Notequal, TokenType.StringEqual );
			tokenList = AddParenthesesForOperator( tokenList, TokenType.AND, TokenType.OR );
			return tokenList;
		}

		private TokenCollection AddParenthesesForOperator( IEnumerable<Token> block, params TokenType[] operatorKeywords ) {
			var expressionToken = new TokenCollection( block.TakeWhile( x => x.Type != TokenType.NewLine ) );
			for( int i = 0; i < expressionToken.Count; i++ ) {
				var getToken = new Func<int, Token>(
				index => {
					if( index >= expressionToken.Count
						|| index < 0 ) {
						return _tokenFactory.CreateUndefined( 0, 0 );
					}
					return expressionToken[index];
				} );
				var insertRightParen = new Action<TokenCollection, int>( ( col, pos ) => col.Insert( pos, _tokenFactory.CreateRightparen( 0, 0 ) ) );
				var insertLeftParen = new Action<TokenCollection, int>(
				( col, pos ) => {
					col.Insert( pos, _tokenFactory.CreateLeftparen( 0, 0 ) );
					i++;
				} );

				if( operatorKeywords.Contains( getToken( i ).Type )
					&& getToken( i-1 ).Type != TokenType.None
					&& !getToken( i-1 ).IsNumberExpression
					&& !getToken( i-1 ).IsMathExpression
					&& getToken( i-1 ).Type != TokenType.Leftparen
					&& getToken( i-1 ).Type != TokenType.Leftsubscript
					&& getToken( i-1 ).Type != TokenType.Comma
					&& getToken( i-1 ).Type != TokenType.QuestionMark
					&& getToken( i-1 ).Type != TokenType.DoubleDot ) {
					switch(getToken( i ).Type) {
						case TokenType.Pow:
						case TokenType.Mult:
						case TokenType.Div:
						case TokenType.Minus:
						case TokenType.Plus:
						case TokenType.Concat:
						case TokenType.Less:
						case TokenType.LessEqual:
						case TokenType.Greater:
						case TokenType.GreaterEqual:
						case TokenType.Equal:
						case TokenType.StringEqual:
						case TokenType.Notequal:
						case TokenType.AND:
						case TokenType.OR:
							int rparenIndex = GetIndexBehindNextToken( getToken, i );
							int lparenIndex = GetIndexBeforeLastToken( getToken, i ).ToNullIfLessNull();

							while( ( getToken( lparenIndex-1 ).IsSignOperator && ( getToken( lparenIndex-2 ).IsSignOperator || getToken( lparenIndex-2 ).Type == TokenType.None ) )
								   || ( getToken( lparenIndex-2 ).Type == TokenType.Leftparen || getToken( lparenIndex-2 ).Type == TokenType.Leftsubscript ) ) {
								lparenIndex--;
							}

							insertRightParen( expressionToken, rparenIndex );
							insertLeftParen( expressionToken, lparenIndex );
							i += 2;
							break;
						case TokenType.NOT:
							int lparenIndexNot = HandleParenthesesForNotExpression( getToken, i );
							insertRightParen( expressionToken, lparenIndexNot );
							insertLeftParen( expressionToken, i );
							i += 2;
							break;
						default:
							throw new SyntaxTreeException( "Unexpected operator", getToken( i ).Col, getToken( i ).Line );
					}
				}
			}
			return expressionToken;
		}

		private int GetIndexBeforeLastToken( Func<int, Token> currentToken, int i ) {
			int seperatorCount = 0;
			Func<bool> isInner = () => seperatorCount > 0;
			switch(currentToken( --i ).Type) {
				case TokenType.Int32:
				case TokenType.Int64:
				case TokenType.Double:
				case TokenType.String:
				case TokenType.Variable:
				case TokenType.Macro:
				case TokenType.Null:
					return i;
				case TokenType.Rightsubscript:
					while( currentToken( i ).Type == TokenType.Rightsubscript ) {
						do {
							if( currentToken( i ).Type == TokenType.Rightsubscript ) {
								seperatorCount++;
							}
							if( currentToken( i ).Type == TokenType.Leftsubscript ) {
								seperatorCount--;
							}
							i--;
						} while( isInner() );
					}
					return i;
				case TokenType.Rightparen:
					while( currentToken( i ).Type == TokenType.Rightparen ) {
						do {
							if( currentToken( i ).Type == TokenType.Rightparen ) {
								seperatorCount++;
							}
							if( currentToken( i ).Type == TokenType.Leftparen ) {
								seperatorCount--;
							}
							i--;
						} while( isInner() );
					}
					switch(currentToken( i ).Type) {
						case TokenType.Function:
						case TokenType.Userfunction:
							return i;
						default:
							return i+1;
					}
			}
			throw new SyntaxTreeException( "unexpected token", currentToken( i ).Col, currentToken( i ).Line );
		}

		private int HandleParenthesesForNotExpression( Func<int, Token> currentToken, int i ) {
			return GetIndexBehindNextToken( currentToken, i );
		}

		private int GetIndexBehindNextToken( Func<int, Token> currentToken, int i ) {
			int seperatorCount = 0;
			Func<bool> isInner = () => seperatorCount > 0;

			while( currentToken( i+1 ).IsSignOperator ) {
				i++;
			}

			switch(currentToken( ++i ).Type) {
				case TokenType.Keyword:
					switch(currentToken( i ).Value.Keyword) {
						case Keywords.Default:
						case Keywords.True:
						case Keywords.False:
							return ++i;
					}
					throw new InvalidOperationException();
				case TokenType.Int32:
				case TokenType.Int64:
				case TokenType.Double:
				case TokenType.String:
				case TokenType.Macro:
				case TokenType.Null:
					return ++i;
				case TokenType.Variable:
					switch(currentToken( ++i ).Type) {
						case TokenType.Leftsubscript:
							while( currentToken( i ).Type == TokenType.Leftsubscript ) {
								do {
									if( currentToken( i ).Type == TokenType.Leftsubscript ) {
										seperatorCount++;
									}
									if( currentToken( i ).Type == TokenType.Rightsubscript ) {
										seperatorCount--;
									}
									i++;
								} while( isInner() );
							}
							break;
					}
					return i;
				case TokenType.Function:
				case TokenType.Userfunction:
					i++;
					do {
						if( currentToken( i ).Type == TokenType.Leftparen ) {
							seperatorCount++;
						}
						if( currentToken( i ).Type == TokenType.Rightparen ) {
							seperatorCount--;
						}
						i++;
					} while( isInner() );
					return i;
				case TokenType.Leftparen:
					do {
						if( currentToken( i ).Type == TokenType.Leftparen ) {
							seperatorCount++;
						}
						if( currentToken( i ).Type == TokenType.Rightparen ) {
							seperatorCount--;
						}
						i++;
					} while( isInner() );
					return i;
				default:
					throw new SyntaxTreeException( "unexpected token", currentToken( i ).Col, currentToken( i ).Line );
			}
		}
	}
}
