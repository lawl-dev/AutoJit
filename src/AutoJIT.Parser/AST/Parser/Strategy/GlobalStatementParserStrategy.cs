using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class GlobalStatementParserStrategy : StatementParserStrategyBase<GlobalDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public GlobalStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, ITokenFactory tokenFactory, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseGlobal( block );
        }

        private IEnumerable<IStatementNode> ParseGlobal( TokenQueue block ) {
            var toReturn = new List<IStatementNode>();
            bool isConst = Skip( block, Keywords.Const );

            while( block.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

                IExpressionNode initExpression = null;
                if( Skip( block, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( block ) ), true );
                }

                toReturn.Add( AutoitStatementFactory.CreateGlobalDeclarationStatement( variableExpression, initExpression, isConst ) );

                Skip( block, TokenType.Comma );
            }
            return toReturn;
        }
    }
}
