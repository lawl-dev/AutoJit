using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class GlobalEnumParserStrategy : StatementParserStrategyBase<GlobalEnumDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public GlobalEnumParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory, ITokenFactory tokenFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseEnum( block );
        }

        private IEnumerable<IStatementNode> ParseEnum( TokenQueue block ) {
            var toReturn = new List<IStatementNode>();

            Token @operator = _tokenFactory.CreatePlus( -1, -1 );

            IExpressionNode left = new NumericLiteralExpression( _tokenFactory.CreateInt( 1, -1, -1 ), new List<Token>() );
            if ( Consume( block, Keywords.Step ) ) {
                @operator = block.Dequeue();
                left = ExpressionParser.ParseSingle<IExpressionNode>( block );
            }

            VariableExpression lastVariableExpression = null;
            while ( block.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

                IExpressionNode initExpression = null;
                if ( Consume( block, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseSingle<IExpressionNode>( new TokenCollection( ExtractUntilNextDeclaration( block ) ) );
                }

                IExpressionNode autoInitExpression = lastVariableExpression == null
                    ? (IExpressionNode) new NumericLiteralExpression(
                        _tokenFactory.CreateInt(
                            @operator.Type == TokenType.Mult
                                ? 1
                                : 0,
                            -1,
                            -1 ),
                        new List<Token>() )
                    : new BinaryExpression( (IExpressionNode) lastVariableExpression.Clone(), (IExpressionNode) left.Clone(), new TokenNode( @operator ) );

                toReturn.Add( AutoitStatementFactory.CreateEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression, true ) );

                lastVariableExpression = variableExpression;

                Consume( block, TokenType.Comma );
            }

            return toReturn;
        }
    }
}
