using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser
{
    public class ScriptParser : ParserBase, IScriptParser
    {
        private readonly IExpressionParser _expressionParser;
        private readonly ILexer _lexer;
        private readonly IStatementParser _statementParser;
        private readonly IAutoitSyntaxFactory _autoitSyntaxFactory;

        public ScriptParser( ILexer lexer, IExpressionParser expressionParser, IStatementParser statementParser, IAutoitSyntaxFactory autoitSyntaxFactory ) {
            _lexer = lexer;
            _expressionParser = expressionParser;
            _statementParser = statementParser;
            _autoitSyntaxFactory = autoitSyntaxFactory;
        }

        public AutoitScriptRoot ParseScript( string script, PragmaOptions pragmaOptions ) {
            TokenCollection token = _lexer.Lex( script );

            var functionToken = GetAutoJITScript( token );

            var functions = new List<Function>();

            foreach (FunctionToken function in functionToken.Where( x=>!x.IsMain )) {
                var functionStatements = _autoitSyntaxFactory.CreateBlockStatement(_statementParser.ParseBlock( new TokenQueue(function.Tokens) ).ToList());
                Function func = _autoitSyntaxFactory.CreateFunction( _autoitSyntaxFactory.CreateTokenNode( function.Name ), function.Parameter, functionStatements );
                functions.Add( func );
            }
            var mainToken = functionToken.Single(x=>x.IsMain);
            var mainStatements = _statementParser.ParseBlock( new TokenQueue( mainToken.Tokens ) );
            var mainBlock = _autoitSyntaxFactory.CreateBlockStatement( mainStatements );
            var autoitScriptRoot = _autoitSyntaxFactory.CreateRoot( functions, mainBlock, pragmaOptions );


            return autoitScriptRoot;
        }

        private List<FunctionToken> GetAutoJITScript( IEnumerable<Token> tokens ) {
            var tokenQueue = new TokenQueue( tokens );

            var main = new FunctionToken( null, new List<AutoitParameter>(), true );

            var functions = new List<FunctionToken>();
            bool isFunctionBody = false;

            while ( tokenQueue.Any() ) {
                Token token = tokenQueue.Dequeue();

                if ( token.Value.Keyword == Keywords.Endfunc ) {
                    if ( isFunctionBody ) {
                        ConsumeAndEnsure( tokenQueue, TokenType.NewLine );
                        isFunctionBody = false;
                    }
                    else {
                        throw new InvalidParseException( 0, 0, "ENDFUNC" );
                    }
                }

                if ( isFunctionBody ) {
                    functions.Last().Tokens.Enqueue( token );
                }
                else if ( token.Value.Keyword == Keywords.Func ) {
                    isFunctionBody = true;
                    Token functionName = tokenQueue.Dequeue();

                    functions.Add(new FunctionToken(functionName, ParseFunctionParameter(tokenQueue).ToList()));
                    ConsumeAndEnsure( tokenQueue, TokenType.NewLine );
                }
                else if ( token.Value.Keyword != Keywords.Endfunc ) {
                    main.Tokens.Enqueue( token );
                }
            }
            functions.Add( main );
            return functions;
        }


        private IEnumerable<AutoitParameter> ParseFunctionParameter( TokenQueue tokenQueue ) {
            var parameterPart = new TokenQueue( GetBetween( tokenQueue, TokenType.Leftparen, TokenType.Rightparen ) );
            var toReturn = new List<AutoitParameter>();

            if ( !parameterPart.Any() ) {
                return toReturn;
            }

            do {
                Consume( parameterPart, TokenType.Comma );

                bool isConst = Consume( parameterPart, Keywords.Const );
                bool isByRef = Consume( parameterPart, Keywords.ByRef );

                Token name = parameterPart.Dequeue();

                IExpressionNode defaultExpression = null;
                if ( parameterPart.Any()
                     &&
                     Consume( parameterPart, TokenType.Equal ) ) {
                    defaultExpression = _expressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( parameterPart ) ), true );
                }
                toReturn.Add(_autoitSyntaxFactory.CreateParameter(name, defaultExpression, isByRef, isConst ) );
            } while ( parameterPart.Any()
                      &&
                      parameterPart.Peek().Type == TokenType.Comma );
            return toReturn;
        }
    }
}
