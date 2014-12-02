using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
    public abstract class StatementParserBase : ParserBase
    {
        protected TokenCollection ParseUntilNewLine( TokenQueue block ) {
            return new TokenCollection( block.DequeueWhile( x => x.Type != TokenType.NewLine ) );
        }

        protected TokenCollection ParseIfCondition( TokenQueue block ) {
            List<Token> innerExpressionsBlock = block.DequeueWhile( x => x.Value.Keyword != Keywords.Then ).ToList();
            ConsumeAndEnsure( block, Keywords.Then );
            return new TokenCollection( innerExpressionsBlock );
        }

        protected TokenCollection ParseIfBlock( TokenQueue block ) {
            List<Token> ifblock = ParseIfBlockUntil( block );
            return new TokenCollection( ifblock );
        }

        protected List<Token> ParseIfBlockUntil( TokenQueue block ) {
            int count = 1;
            bool nextIsCaseElse = false;
            var res = new TokenCollection(
                block.DequeueWhile(
                    ( token, i ) => {
                        bool hasBlock;
                        List<Token> line;
                        switch (token.Value.Keyword) {
                            case Keywords.Then:
                                Token nextToken = block.Skip( 1 ).FirstOrDefault();
                                hasBlock = nextToken != null && nextToken.Type == TokenType.NewLine;
                                if ( hasBlock ) {
                                    count++;
                                }
                                break;
                            case Keywords.ElseIf:
                                line = block.TakeWhile( x => x.Type != TokenType.NewLine ).ToList();
                                hasBlock = line[line.Count-1].Value.Keyword == Keywords.Then;
                                if ( hasBlock ) {
                                    count--;
                                }
                                break;
                            case Keywords.EndIf:
                                count--;
                                break;
                            case Keywords.Else:
                                line = block.TakeWhile( x => x.Type != TokenType.NewLine ).ToList();
                                hasBlock = line.Count == 1;
                                bool isOuterLoop = count == 1;
                                if ( hasBlock
                                     &&
                                     isOuterLoop
                                     &&
                                     !nextIsCaseElse ) {
                                    count--;
                                }
                                if ( nextIsCaseElse ) {
                                    nextIsCaseElse = false;
                                }
                                break;
                            case Keywords.Case:
                                nextIsCaseElse = block.Skip( 1 ).First().Value.Keyword == Keywords.Else;
                                break;
                        }
                        return count != 0;
                    } ) );
            return res;
        }

        protected TokenCollection ParseElseIfCondition( TokenQueue block ) {
            return ParseIfCondition( block );
        }

        protected TokenCollection ParseElseIfBlock( TokenQueue block ) {
            List<Token> elseIfblock = ParseIfBlockUntil( block );
            return new TokenCollection( elseIfblock );
        }

        protected TokenCollection ParseElseBlock( TokenQueue block ) {
            List<Token> elseIfblock = ParseIfBlockUntil( block );
            return new TokenCollection( elseIfblock );
        }


        protected TokenCollection ParseInnerUntilSwitchSelect( TokenQueue block ) {
            int count = 1;
            var res = new TokenCollection(
                block.DequeueWhile(
                    delegate( Token token, int i ) {
                        switch (token.Value.Keyword) {
                                case Keywords.Switch:
                                case Keywords.Select:
                                count++;
                                break;
                            case Keywords.EndSelect:
                            case Keywords.EndSwitch:
                                count--;
                                break;
                            case Keywords.Case:
                                var hasNoAncestors = count == 1;
                                if ( hasNoAncestors )
                                    count--;
                                break;
                        }
                        return count != 0;
                    } ) );

            return res;
        }
    }
}
