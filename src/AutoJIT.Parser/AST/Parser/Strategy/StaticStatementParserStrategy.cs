using System.Collections.Generic;
using System.Linq;
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
    public sealed class StaticStatementParserStrategy : StatementParserStrategyBase<StaticDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public StaticStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, ITokenFactory tokenFactory, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseLocal( block );
        }

        private IEnumerable<IStatementNode> ParseLocal( TokenQueue block ) {
            var line = GetLine( block );


            var toReturn = new List<IStatementNode>();

            while ( line.Any()
                    &&
                    line.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( line );

                IExpressionNode initExpression = null;
                if ( Consume( line, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( 
                        line ) ), true );
                }
                toReturn.Add( AutoitSyntaxFactory.CreateStaticDeclarationStatement( variableExpression, initExpression ) );

                Consume( line, TokenType.Comma );
            }

            Ensure(() => !line.Any());
            return toReturn;
        }
    }
}
