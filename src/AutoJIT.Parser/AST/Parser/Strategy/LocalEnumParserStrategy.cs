using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class LocalEnumParserStrategy : StatementParserStrategyBase<LocalEnumDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public LocalEnumParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory, ITokenFactory tokenFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseEnum( block );
        }

        private IEnumerable<IStatementNode> ParseEnum( TokenQueue block ) {
            var line = GetLine( block );

            var toReturn = new List<IStatementNode>();

            Token @operator = _tokenFactory.CreatePlus( -1, -1 );

            IExpressionNode left = AutoitSyntaxFactory.CreateNumericLiteralExpression( AutoitSyntaxFactory.CreateTokenNode( 1 ), Constants.Array<TokenNode>.Empty.ToList() );
            if ( Consume( line, Keywords.Step ) ) {
                @operator = line.Dequeue();
                left = ExpressionParser.ParseSingle<IExpressionNode>( line );
            }

            VariableExpression lastVariableExpression = null;
            while (line.Any() && line.Peek().Type == TokenType.Variable)
            {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( line );

                IExpressionNode initExpression = null;
                if ( Consume( line, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseSingle<IExpressionNode>( new TokenCollection( ExtractUntilNextDeclaration( line ) ) );
                }

                IExpressionNode autoInitExpression = lastVariableExpression == null
                    ? (IExpressionNode) AutoitSyntaxFactory.CreateNumericLiteralExpression(
                        AutoitSyntaxFactory.CreateTokenNode(
                            @operator.Type == TokenType.Mult
                                ? 1
                                : 0 ), Constants.Array<TokenNode>.Empty.ToList() )
                    : AutoitSyntaxFactory.CreateBinaryExpression( (IExpressionNode) lastVariableExpression.Clone(), (IExpressionNode) left.Clone(), AutoitSyntaxFactory.CreateTokenNode( @operator ) );

                toReturn.Add( AutoitSyntaxFactory.CreateEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression, false ) );

                lastVariableExpression = variableExpression;

                Consume( line, TokenType.Comma );
            }

            Ensure(() => !line.Any());
            return toReturn;
        }
    }
}
