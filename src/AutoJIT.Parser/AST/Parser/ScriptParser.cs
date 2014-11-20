using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
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
                var functionStatements = _statementParser.ParseBlock( new TokenQueue(function.Tokens) ).ToList();
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

            var main = new FunctionToken( null, new List<AutoitParameterInfo>(), true );

            var functions = new List<FunctionToken>();
            bool isFunctionBody = false;

            while ( tokenQueue.Any() ) {
                Token token = tokenQueue.Dequeue();

                if ( token.Value.Keyword == Keywords.Endfunc ) {
                    if ( isFunctionBody ) {
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
                }
                else if ( token.Value.Keyword != Keywords.Endfunc ) {
                    main.Tokens.Enqueue( token );
                }
            }
            functions.Add( main );
            return functions;
        }


        private IEnumerable<AutoitParameterInfo> ParseFunctionParameter( TokenQueue tokenQueue ) {
            var parameterPart = new TokenQueue( ParseInner( tokenQueue, TokenType.Leftparen, TokenType.Rightparen ) );
            var toReturn = new List<AutoitParameterInfo>();

            if ( !parameterPart.Any() ) {
                return toReturn;
            }

            do {
                Consume( parameterPart, TokenType.Comma );

                bool isConst = Consume( parameterPart, Keywords.Const );
                bool isByRef = Consume( parameterPart, Keywords.ByRef );

                string name = parameterPart.Dequeue().Value.StringValue;

                IExpressionNode initExpression = null;
                if ( parameterPart.Any()
                     &&
                     Consume( parameterPart, TokenType.Equal ) ) {
                    initExpression = _expressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( parameterPart ) ), true );
                }
                toReturn.Add( new AutoitParameterInfo( name, initExpression, isByRef, isConst ) );
            } while ( parameterPart.Any()
                      &&
                      parameterPart.Peek().Type == TokenType.Comma );
            return toReturn;
        }
    }

    internal class FunctionToken {
        public Token Name { get; private set; }
        public List<AutoitParameterInfo> Parameter { get; private set; }
        public bool IsMain { get; private set; }
        public Queue<Token> Tokens { get; private set; }
        

        public FunctionToken( Token name, List<AutoitParameterInfo> parameter, bool isMain = false ) {
            Name = name;
            Parameter = parameter;
            IsMain = isMain;
            Tokens = new Queue<Token>();
        }
    }
}
