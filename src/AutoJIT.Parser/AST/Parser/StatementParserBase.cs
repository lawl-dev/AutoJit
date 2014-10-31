using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser
{
	public abstract class StatementParserBase : ParserBase
	{
		protected TokenCollection ParseForToStartExpression( TokenQueue block ) {
			var toReturn = new TokenCollection( block.DequeueWhile( x => x.Value.Keyword != Keywords.To ) );
			ConsumeAndEnsure( block, Keywords.To );
			return toReturn;
		}

		protected TokenCollection ParseForToStopExpression( TokenQueue block ) {
			var toReturn = new TokenCollection( block.DequeueWhile( x => x.Value.Keyword != Keywords.Step && x.Type != TokenType.NewLine ) );
			return toReturn;
		}

		protected TokenCollection ParseForToStepExpression( TokenQueue block ) {
			return new TokenCollection( block.DequeueWhile( x => x.Type != TokenType.NewLine ) );
		}

		protected TokenCollection ParseForToStatements( TokenQueue block ) {
			return ParseInner( block, Keywords.For, Keywords.Next, true );
		}

		protected TokenCollection ParseUntilNewLine( TokenQueue block ) {
			return new TokenCollection( block.DequeueWhile( x => x.Type != TokenType.NewLine ) );
		}

		protected TokenCollection ParseWhileExpression( TokenQueue block ) {
			var whileExpression = new TokenCollection( block.DequeueWhile( x => x.Type != TokenType.NewLine ).Where( x => x.Value.Keyword != Keywords.While ) );

			return whileExpression;
		}

		protected TokenCollection ParseWhileBlock( TokenQueue block ) {
			return ParseInner( block, Keywords.While, Keywords.Wend, true );
		}

		protected TokenCollection ParseIfCondition( TokenQueue block ) {
			List<Token> innerExpressionsBlock = block.DequeueWhile( x => x.Value.Keyword != Keywords.Then ).ToList();
			ConsumeAndEnsure( block, Keywords.Then );
			return new TokenCollection( innerExpressionsBlock );
		}

		protected TokenCollection ParseIfBlock( TokenQueue block ) {
			IEnumerable<Token> ifblock = ParseIfBlockUntil( block );
			return new TokenCollection( ifblock );
		}

		protected IEnumerable<Token> ParseIfBlockUntil( TokenQueue block ) {
			int count = 1;
			bool nextIsCaseElse = false;
			var res = new TokenCollection(
			block.DequeueWhile(
							   ( token, i ) => {
								   bool hasBlock;
								   List<Token> line;
								   switch(token.Value.Keyword) {
									   case Keywords.Then:
										   Token nextToken = block.Skip( 1 ).FirstOrDefault();
										   hasBlock = nextToken != null && nextToken.Type == TokenType.NewLine;
										   if( hasBlock ) {
											   count++;
										   }
										   break;
									   case Keywords.ElseIf:
										   line = block.TakeWhile( x => x.Type != TokenType.NewLine ).ToList();
										   hasBlock = line[line.Count-1].Value.Keyword == Keywords.Then;
										   if( hasBlock ) {
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
										   if( hasBlock
											   && isOuterLoop
											   && !nextIsCaseElse ) {
											   count--;
										   }
										   if( nextIsCaseElse ) {
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
			IEnumerable<Token> elseIfblock = ParseIfBlockUntil( block );
			return new TokenCollection( elseIfblock );
		}

		protected TokenCollection ParseElseBlock( TokenQueue block ) {
			IEnumerable<Token> elseIfblock = ParseIfBlockUntil( block );
			return new TokenCollection( elseIfblock );
		}

		protected List<TokenCollection> ParseFunctionParameter( TokenQueue block ) {
			TokenCollection innerExpressionsBlock = ParseInner( block, TokenType.Leftparen, TokenType.Rightparen );

			var list = new List<TokenCollection> {
				new TokenCollection()
			};
			foreach(Token token in innerExpressionsBlock) {
				if( token.Type == TokenType.Comma ) {
					list.Add( new TokenCollection() );
				}
				else {
					list.Last().Add( token );
				}
			}

			return list;
		}

		protected TokenCollection ParseVariableAssignExpression( TokenQueue block ) {
			return new TokenCollection( block.DequeueUntil( x => x.Type == TokenType.NewLine ) );
		}

		protected TokenCollection ParseInnerUntilSwitchSelect( TokenQueue block ) {
			int count = 1;
			var res = new TokenCollection(
			block.DequeueWhile(
							   delegate( Token token, int i ) {
								   if( token.Value.Keyword == Keywords.Switch
									   || token.Value.Keyword == Keywords.Select ) {
									   count++;
								   }
								   else if( token.Value.Keyword == Keywords.Case
											&& count == 1 ) {
									   count--;
								   }
								   else if( token.Value.Keyword == Keywords.Endselect
											|| token.Value.Keyword == Keywords.EndSwitch ) {
									   count--;
								   }
								   return count != 0;
							   } ) );

			return res;
		}
	}
}
