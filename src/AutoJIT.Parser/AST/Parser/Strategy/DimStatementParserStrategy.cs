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

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class DimStatementParserStrategy : StatementParserStrategyBase<DimStatement>
    {
        public DimStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseDim( block );
        }

        private IEnumerable<DimStatement> ParseDim( TokenQueue block ) {
            var toReturn = new List<DimStatement>();

            var lineBlock = new TokenQueue(block.DequeueWhile( x => x.Type != TokenType.NewLine ));

            while (lineBlock.Any() && lineBlock.Peek().Type == TokenType.Variable)
            {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>(lineBlock);

                IExpressionNode initExpression = null;
                if (Consume(lineBlock, TokenType.Equal))
                {
                    initExpression = ExpressionParser.ParseBlock(new TokenCollection(ExtractUntilNextDeclaration(lineBlock)), true);
                }

                toReturn.Add( AutoitSyntaxFactory.CreateDimStatement( variableExpression, initExpression ) );

                Consume( lineBlock, TokenType.Comma );
            }

            ConsumeAndEnsure( block, TokenType.NewLine );
            return toReturn;
        }
    }
}
