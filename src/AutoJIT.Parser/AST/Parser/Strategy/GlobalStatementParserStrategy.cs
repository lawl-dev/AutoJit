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
    public sealed class GlobalStatementParserStrategy : StatementParserStrategyBase<GlobalDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public GlobalStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, ITokenFactory tokenFactory, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseGlobal( block );
        }

        private IEnumerable<IStatementNode> ParseGlobal( TokenQueue block ) {
            var lineBlock = new TokenQueue( block.DequeueWhile( x=>x.Type != TokenType.NewLine ) );

            var toReturn = new List<IStatementNode>();
            bool isConst = Consume( lineBlock, Keywords.Const );

            while (lineBlock.Any() && lineBlock.Peek().Type == TokenType.Variable)
            {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( lineBlock );

                IExpressionNode initExpression = null;
                if ( Consume( lineBlock, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseBlock( new TokenCollection( ExtractUntilNextDeclaration( lineBlock ) ), true );
                }

                toReturn.Add( AutoitSyntaxFactory.CreateGlobalDeclarationStatement( variableExpression, initExpression, isConst ) );

                Consume( lineBlock, TokenType.Comma );
            }

            Ensure(() => !lineBlock.Any());
            ConsumeAndEnsure( block, TokenType.NewLine );
            return toReturn;
        }
    }
}
