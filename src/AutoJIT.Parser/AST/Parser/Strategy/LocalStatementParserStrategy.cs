using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class LocalStatementParserStrategy : StatementParserStrategyBase<LocalDeclarationStatement>
    {
        public LocalStatementParserStrategy(
        IStatementParser statementParser,
        IExpressionParser expressionParser,
        IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseLocal( block );
        }

        private IEnumerable<IStatementNode> ParseLocal( TokenQueue block ) {
            var toReturn = new List<IStatementNode>();

            bool isConst = Consume( block, Keywords.Const );

            while( block.Any()
                   && block.Peek().Type == TokenType.Variable ) {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

                IExpressionNode initExpression = null;
                if( Consume( block, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( block ) ), true );
                }
                toReturn.Add( AutoitStatementFactory.CreateLocalDeclarationStatement( variableExpression, initExpression, isConst ) );

                Consume( block, TokenType.Comma );
            }
            return toReturn;
        }
    }
}
