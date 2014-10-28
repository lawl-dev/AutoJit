using System.Collections.Generic;
using System.Linq;
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
    public sealed class StaticStatementParserStrategy : StatementParserStrategyBase<StaticDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public StaticStatementParserStrategy(
        IStatementParser statementParser,
        IExpressionParser expressionParser,
        ITokenFactory tokenFactory,
        IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseLocal( block );
        }

        private IEnumerable<IStatementNode> ParseLocal( TokenQueue block ) {
            var toReturn = new List<IStatementNode>();


            while( block.Any()
                   && block.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

                IExpressionNode initExpression = null;
                if( Consume( block, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( block ) ), true );
                }
                toReturn.Add( AutoitStatementFactory.CreateStaticDeclarationStatement( variableExpression, initExpression ) );

                Consume( block, TokenType.Comma );
            }
            return toReturn;
        }
    }
}