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
            TokenCollection tokenList = AddParenthesesForOperator( expression, TokenType.Not );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.Pow );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.Mult, TokenType.Div );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.Plus, TokenType.Minus );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.Concat );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual, TokenType.Equal, TokenType.Notequal, TokenType.StringEqual );
            tokenList = AddParenthesesForOperator( tokenList, TokenType.And, TokenType.Or );
            return tokenList;
        }

        private TokenCollection AddParenthesesForOperator( IEnumerable<Token> block, params TokenType[] operatorKeywords ) {
            var expressionToken = new TokenCollection( block.TakeWhile( x => x.Type != TokenType.NewLine ).ToList() );

            for ( int i = 0; i < expressionToken.Count; i++ ) {

                var tokenAt = new Func<int, Token>(
                    index => {
                        if ( index >= expressionToken.Count
                             ||
                             index < 0 ) {
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

                if ( operatorKeywords.Contains( tokenAt( i ).Type )
                     &&
                     tokenAt( i-1 ).Type != TokenType.None
                     &&
                     !tokenAt( i-1 ).IsNumberExpression
                     &&
                     !tokenAt( i-1 ).IsMathExpression
                     &&
                     tokenAt( i-1 ).Type != TokenType.Leftparen
                     &&
                     tokenAt( i-1 ).Type != TokenType.Leftsubscript
                     &&
                     tokenAt( i-1 ).Type != TokenType.Comma
                     &&
                     tokenAt( i-1 ).Type != TokenType.QuestionMark
                     &&
                     tokenAt( i-1 ).Type != TokenType.DoubleDot ) {
                    switch (tokenAt( i ).Type) {
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
                        case TokenType.And:
                        case TokenType.Or:
                            int rparenIndex = GetIndexBehindNextToken( tokenAt, i );
                            int lparenIndex = GetIndexBeforeLastToken( tokenAt, i ).ToNullIfLessNull();

                            while ( ( tokenAt( lparenIndex-1 ).IsSignOperator && ( tokenAt( lparenIndex-2 ).IsSignOperator || tokenAt( lparenIndex-2 ).Type == TokenType.None ) )
                                    ||
                                    ( tokenAt( lparenIndex-2 ).Type == TokenType.Leftparen || tokenAt( lparenIndex-2 ).Type == TokenType.Leftsubscript ) ) {
                                lparenIndex--;
                            }

                            insertRightParen( expressionToken, rparenIndex );
                            insertLeftParen( expressionToken, lparenIndex );
                            i += 2;
                            break;
                        case TokenType.Not:
                            int lparenIndexNot = HandleParenthesesForNotExpression( tokenAt, i );
                            insertRightParen( expressionToken, lparenIndexNot );
                            insertLeftParen( expressionToken, i );
                            i += 2;
                            break;
                        default:
                            throw new SyntaxTreeException( "Unexpected operator", tokenAt( i ).Col, tokenAt( i ).Line );
                    }
                }
            }
            return expressionToken;
        }

        private int GetIndexBeforeLastToken( Func<int, Token> tokenAt, int i ) {
            int seperatorCount = 0;
            Func<bool> isInner = () => seperatorCount > 0;
            switch (tokenAt( --i ).Type) {
                case TokenType.Int32:
                case TokenType.Int64:
                case TokenType.Double:
                case TokenType.String:
                case TokenType.Variable:
                case TokenType.Macro:
                case TokenType.Null:
                    return i;
                case TokenType.Rightsubscript:
                    while ( tokenAt( i ).Type == TokenType.Rightsubscript ) {
                        do {
                            if ( tokenAt( i ).Type == TokenType.Rightsubscript ) {
                                seperatorCount++;
                            }
                            if ( tokenAt( i ).Type == TokenType.Leftsubscript ) {
                                seperatorCount--;
                            }
                            i--;
                        } while ( isInner() );
                    }
                    return i;
                case TokenType.Rightparen:
                    while ( tokenAt( i ).Type == TokenType.Rightparen ) {
                        do {
                            if ( tokenAt( i ).Type == TokenType.Rightparen ) {
                                seperatorCount++;
                            }
                            if ( tokenAt( i ).Type == TokenType.Leftparen ) {
                                seperatorCount--;
                            }
                            i--;
                        } while ( isInner() );
                    }
                    switch (tokenAt( i ).Type) {
                        case TokenType.Function:
                        case TokenType.Userfunction:
                            return i;
                        default:
                            return i+1;
                    }
            }
            throw new SyntaxTreeException( "unexpected token", tokenAt( i ).Col, tokenAt( i ).Line );
        }

        private int HandleParenthesesForNotExpression( Func<int, Token> currentToken, int i ) {
            return GetIndexBehindNextToken( currentToken, i );
        }

        private int GetIndexBehindNextToken( Func<int, Token> tokenAt, int i ) {
            int seperatorCount = 0;
            Func<bool> isInner = () => seperatorCount > 0;

            while ( tokenAt( i+1 ).IsSignOperator ) {
                i++;
            }

            switch (tokenAt( ++i ).Type) {
                case TokenType.Keyword:
                    switch (tokenAt( i ).Value.Keyword) {
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
                    switch (tokenAt( ++i ).Type) {
                        case TokenType.Leftsubscript:
                            while ( tokenAt( i ).Type == TokenType.Leftsubscript ) {
                                do {
                                    if ( tokenAt( i ).Type == TokenType.Leftsubscript ) {
                                        seperatorCount++;
                                    }
                                    if ( tokenAt( i ).Type == TokenType.Rightsubscript ) {
                                        seperatorCount--;
                                    }
                                    i++;
                                } while ( isInner() );
                            }
                            break;
                    }
                    return i;
                case TokenType.Function:
                case TokenType.Userfunction:
                    i++;
                    do {
                        if ( tokenAt( i ).Type == TokenType.Leftparen ) {
                            seperatorCount++;
                        }
                        if ( tokenAt( i ).Type == TokenType.Rightparen ) {
                            seperatorCount--;
                        }
                        i++;
                    } while ( isInner() );
                    return i;
                case TokenType.Leftparen:
                    do {
                        if ( tokenAt( i ).Type == TokenType.Leftparen ) {
                            seperatorCount++;
                        }
                        if ( tokenAt( i ).Type == TokenType.Rightparen ) {
                            seperatorCount--;
                        }
                        i++;
                    } while ( isInner() );
                    return i;
                default:
                    throw new SyntaxTreeException( "unexpected token", tokenAt( i ).Col, tokenAt( i ).Line );
            }
        }
    }
}
