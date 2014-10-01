using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class DimStatementParserStrategy : StatementParserStrategyBase<DimStatement>
    {
        public DimStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseDim( block );
        }

        private IEnumerable<DimStatement> ParseDim( TokenQueue block ) {
            var toReturn = new List<DimStatement>();
            while ( block.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

                IExpressionNode initExpression = null;
                if ( Skip( block, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( block ) ), true );
                }

                toReturn.Add( AutoitStatementFactory.CreateDimStatement( variableExpression, initExpression ) );

                Skip( block, TokenType.Comma );
            }
            return toReturn;
        }
    }
}
