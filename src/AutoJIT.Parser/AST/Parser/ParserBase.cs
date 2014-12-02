using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    public abstract class ParserBase
    {
        protected static TokenCollection GetBetween( TokenQueue block, TokenType s, TokenType e ) {
            ConsumeAndEnsure( block, s );

            int count = 1;
            List<Token> innerExpressionsBlock = block.DequeueWhile(
                ( x, i ) => {
                    if ( x.Type == s ) {
                        count++;
                    }
                    if ( x.Type == e ) {
                        count--;
                    }
                    return count != 0;
                } ).ToList();

            ConsumeAndEnsure( block, e );

            return new TokenCollection( innerExpressionsBlock );
        }

        protected TokenCollection GetBetween( TokenQueue block, Keywords s, Keywords e, bool sOver = false ) {
            if ( !sOver ) {
                ConsumeAndEnsure( block, s );
            }

            int count = 1;
            var res = new TokenCollection(
                block.DequeueWhile(
                    delegate( Token token, int i ) {
                        if ( token.Value.Keyword == s ) {
                            count++;
                        }
                        else if ( token.Value.Keyword == e ) {
                            count--;
                        }
                        return count != 0;
                    } ) );

            ConsumeAndEnsure( block, e );

            return res;
        }

        protected TokenCollection ParseInnerUntil( TokenQueue block, Keywords s, Keywords e, bool sOver = false ) {
            if ( !sOver ) {
                ConsumeAndEnsure( block, s );
            }

            int count = 1;
            var res = new TokenCollection(
                block.DequeueWhile(
                    delegate( Token token, int i ) {
                        if ( token.Value.Keyword == s ) {
                            count++;
                        }
                        else if ( token.Value.Keyword == e ) {
                            count--;
                        }
                        return count != 0;
                    } ) );

            return res;
        }

        protected IEnumerable<Token> ParseInnerUntil( TokenQueue block, Keywords[] keywordses, Keywords[] keywordses1, bool sOver, Keywords[] ignoreIfInner ) {
            if ( !sOver ) {
                keywordses.Any( x => Consume( block, x ) );
            }

            int count = 1;
            var res = new TokenCollection(
                block.DequeueWhile(
                    delegate( Token token, int i ) {
                        if ( count > 1
                             &&
                             ignoreIfInner.Contains( token.Value.Keyword ) ) {
                            return true;
                        }
                        if ( keywordses.Contains( token.Value.Keyword ) ) {
                            count++;
                        }
                        if ( keywordses1.Contains( token.Value.Keyword ) ) {
                            count--;
                        }

                        return count != 0;
                    } ) );

            return res;
        }

        protected static void ConsumeAndEnsure( TokenQueue block, TokenType tokenType ) {
            if ( block.Peek().Type != tokenType ) {
                throw new SyntaxTreeException( string.Format( "Expected {0} but was {1}: {2}", tokenType, block.Peek().Type, block.Peek().Value.CurrentValue ), block.Peek().Col, block.Peek().Line );
            }
            block.Dequeue();
        }

        protected static void Ensure(Func<bool> func)
        {
            if (!func.Invoke())
            {
                throw new SyntaxTreeException(string.Empty, 0, 0);
            }
        }

        protected static bool Consume( TokenQueue block, TokenType tokenType ) {
            if ( !block.Any() || block.Peek().Type != tokenType ) {
                return false;
            }
            block.Dequeue();
            return true;
        }

        protected bool Consume( TokenQueue block, Keywords tokenType ) {
            if (!block.Any() || block.Peek().Value.Keyword != tokenType ) {
                return false;
            }
            block.Dequeue();
            return true;
        }

        protected static void ConsumeAndEnsure( TokenQueue block, Keywords tokenType ) {
            block.DequeueWhile( x => x.Type == TokenType.NewLine ).ToList();
            if ( block.Peek().Value.Keyword != tokenType ) {
                throw new SyntaxTreeException( string.Format( "Expected {0} but was {1}", tokenType, block.Peek().Value.Keyword ), block.Peek().Col, block.Peek().Line );
            }
            block.Dequeue();
        }

        protected List<Token> ExtractUntilNextDeclaration( TokenQueue block ) {
            var toReturn = new List<Token>();
            bool isPartOfDeclaration;
            int iC = 0;
            do {
                if ( block.Peek().Type == TokenType.Leftparen ) {
                    iC++;
                }
                if ( block.Peek().Type == TokenType.Rightparen ) {
                    iC--;
                }

                if ( block.Peek().Type == TokenType.Leftsubscript ) {
                    iC++;
                }
                if ( block.Peek().Type == TokenType.Rightsubscript ) {
                    iC--;
                }
                bool isInner = iC > 0;
                isPartOfDeclaration = isInner || ( block.Peek().Type != TokenType.Comma && block.Peek().Type != TokenType.NewLine );

                if ( isPartOfDeclaration ) {
                    toReturn.Add( block.Dequeue() );
                }
            } while ( isPartOfDeclaration && block.Any() );
            return toReturn;
        }

        public TokenQueue GetLine( TokenQueue queue ) {
            var token = queue.DequeueWhile( x=>x.Type != TokenType.NewLine );
            ConsumeAndEnsure( queue, TokenType.NewLine );
            return new TokenQueue(token);
        }

        protected bool ConsumeNewLine( TokenQueue queue ) {
            return Consume( queue, TokenType.NewLine );
        }
    }
}
