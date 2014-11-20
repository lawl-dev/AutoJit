using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class GlobalEnumParserStrategy : StatementParserStrategyBase<GlobalEnumDeclarationStatement>
    {
        private readonly ITokenFactory _tokenFactory;

        public GlobalEnumParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory, ITokenFactory tokenFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {
            _tokenFactory = tokenFactory;
        }

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseEnum( block );
        }

        private IEnumerable<IStatementNode> ParseEnum( TokenQueue block ) {
            var lineBlock = new TokenQueue(block.DequeueWhile( x=>x.Type != TokenType.NewLine ));

            var toReturn = new List<IStatementNode>();

            Token @operator = _tokenFactory.CreatePlus( -1, -1 );

            IExpressionNode left = AutoitSyntaxFactory.CreateNumericLiteralExpression( AutoitSyntaxFactory.CreateTokenNode( 1 ), Constants.Array<TokenNode>.Empty.ToList() );
            if ( Consume( lineBlock, Keywords.Step ) ) {
                @operator = lineBlock.Dequeue();
                left = ExpressionParser.ParseSingle<IExpressionNode>( lineBlock );
            }

            VariableExpression lastVariableExpression = null;
            while (lineBlock.Any() && lineBlock.Peek().Type == TokenType.Variable)
            {
                var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( lineBlock );

                IExpressionNode initExpression = null;
                if ( Consume( lineBlock, TokenType.Equal ) ) {
                    initExpression = ExpressionParser.ParseSingle<IExpressionNode>( new TokenCollection( ExtractUntilNextDeclaration( lineBlock ) ) );
                }

                IExpressionNode autoInitExpression = lastVariableExpression == null
                    ? (IExpressionNode) AutoitSyntaxFactory.CreateNumericLiteralExpression(
                        AutoitSyntaxFactory.CreateTokenNode(
                            @operator.Type == TokenType.Mult
                                ? 1
                                : 0 ), Constants.Array<TokenNode>.Empty.ToList() )
                    : AutoitSyntaxFactory.CreateBinaryExpression( (IExpressionNode) lastVariableExpression.Clone(), (IExpressionNode) left.Clone(), AutoitSyntaxFactory.CreateTokenNode( @operator ) );

                toReturn.Add( AutoitSyntaxFactory.CreateEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression, true ) );

                lastVariableExpression = variableExpression;

                Consume( lineBlock, TokenType.Comma );
            }



            Ensure( () => !lineBlock.Any() );
            ConsumeAndEnsure( block, TokenType.NewLine );

            return toReturn;
        }
    }
}
