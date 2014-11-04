using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser
{
	public sealed class ScriptParser : ParserBase, IScriptParser
	{
		private readonly IExpressionParser _expressionParser;
		private readonly ILexer _lexer;
		private readonly IStatementParser _statementParser;

		public ScriptParser( ILexer lexer, IExpressionParser expressionParser, IStatementParser statementParser ) {
			_lexer = lexer;
			_expressionParser = expressionParser;
			_statementParser = statementParser;
		}

		public AutoitScriptRoot ParseScript( string script, PragmaOptions pragmaOptions ) {
			TokenCollection token = _lexer.Lex( script );

			AutoitScriptRoot autoJITScript = GetAutoJITScript( token, pragmaOptions );

			foreach(Function function in autoJITScript.Functions) {
				function.Statements = _statementParser.ParseBlock( function.Queue ).ToList();
				function.Statements.Add( new ReturnStatement( new NullExpression() ) );
			}
			autoJITScript.MainFunction.Statements = _statementParser.ParseBlock( autoJITScript.MainFunction.Queue ).ToList();
			autoJITScript.MainFunction.Statements.Add( new ReturnStatement( new NullExpression() ) );
			return autoJITScript;
		}

		private AutoitScriptRoot GetAutoJITScript( IEnumerable<Token> tokens, PragmaOptions pragmaOptions ) {
			var tokenQueue = new TokenQueue( tokens );

			var main = new Function( "Main", new List<AutoitParameterInfo>() );

			var functions = new List<Function>();
			bool isFunctionBody = false;

			while( tokenQueue.Any() ) {
				Token token = tokenQueue.Dequeue();

				if( token.Value.Keyword == Keywords.Endfunc ) {
					if( isFunctionBody ) {
						isFunctionBody = false;
					}
					else {
						throw new InvalidParseException( 0, 0, "ENDFUNC" );
					}
				}

				if( isFunctionBody ) {
					functions.Last().Queue.Enqueue( token );
				}
				else if( token.Value.Keyword == Keywords.Func ) {
					isFunctionBody = true;
					Token functionName = tokenQueue.Dequeue();
					string name = functionName.Value.StringValue;

					functions.Add( new Function( name, ParseFunctionParameter( tokenQueue ) ) );
				}
				else if( token.Value.Keyword != Keywords.Endfunc ) {
					main.Queue.Enqueue( token );
				}
			}
			return new AutoitScriptRoot( functions, main, pragmaOptions );
		}

		private IEnumerable<AutoitParameterInfo> ParseFunctionParameter( TokenQueue tokenQueue ) {
			var parameterPart = new TokenQueue( ParseInner( tokenQueue, TokenType.Leftparen, TokenType.Rightparen ) );
			var toReturn = new List<AutoitParameterInfo>();

			if( !parameterPart.Any() ) {
				return toReturn;
			}

			do {
				Consume( parameterPart, TokenType.Comma );

				bool isConst = Consume( parameterPart, Keywords.Const );
				bool isByRef = Consume( parameterPart, Keywords.ByRef );

				string name = parameterPart.Dequeue().Value.StringValue;

				IExpressionNode initExpression = null;
				if( parameterPart.Any()
					&& Consume( parameterPart, TokenType.Equal ) ) {
					initExpression = _expressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( parameterPart ) ), true );
				}
				toReturn.Add( new AutoitParameterInfo( name, initExpression, isByRef, isConst ) );
			} while( parameterPart.Any()
					 && parameterPart.Peek().Type == TokenType.Comma );
			return toReturn;
		}
	}
}
