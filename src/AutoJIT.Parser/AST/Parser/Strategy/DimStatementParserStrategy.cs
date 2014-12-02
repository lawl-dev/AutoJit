using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            var line = GetLine( block );

            while (!ConsumeNewLine( line ))
            {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>(line);

                IExpressionNode initExpression = null;
                if (Consume(line, TokenType.Equal))
                {
                    initExpression = ExpressionParser.ParseBlock(new TokenCollection(ExtractUntilNextDeclaration(line)), true);
                }

                toReturn.Add( AutoitSyntaxFactory.CreateDimStatement( variableExpression, initExpression ) );

                Consume( line, TokenType.Comma );
            }

            Ensure( () => !line.Any() );

            return toReturn;
        }
    }
}
