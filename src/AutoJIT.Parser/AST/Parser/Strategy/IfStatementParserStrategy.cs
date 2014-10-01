using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class IfStatementParserStrategy : StatementParserStrategyBase<IfElseStatement>
    {
        public IfStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory )
            : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseIf( block ).ToEnumerable();
        }

        private IfElseStatement ParseIf( TokenQueue block ) {
            IList<TokenCollection> elseIfcondition = new List<TokenCollection>();
            IList<TokenCollection> elseIfBlock = new List<TokenCollection>();
            TokenCollection elseBlock = null;
            TokenCollection condition = ParseIfCondition( block );
            bool lastBlockWasLine = false;
            TokenCollection ifBlock;
            if ( Skip( block, TokenType.NewLine ) ) {
                ifBlock = ParseIfBlock( block );
            }
            else {
                ifBlock = ParseIfLineBlock( block );
                lastBlockWasLine = true;
            }

            var hasElseIf = block.Any() && block.Peek().Value.Keyword == Keywords.ElseIf;
            while ( hasElseIf ) {
                SkipAndAssert( block, Keywords.ElseIf );
                var elseIfCondition = ParseElseIfCondition( block );
                elseIfcondition.Add( elseIfCondition );

                TokenCollection elseIfBlock2;
                if ( Skip( block, TokenType.NewLine ) ) {
                    elseIfBlock2 = ParseElseIfBlock( block );
                    lastBlockWasLine = false;
                }
                else {
                    elseIfBlock2 = ParseElseIfLineBlock( block );
                    SkipAndAssert( block, TokenType.NewLine );
                    lastBlockWasLine = true;
                }

                elseIfBlock.Add( elseIfBlock2 );
                hasElseIf = block.Any() && block.Peek().Value.Keyword == Keywords.ElseIf;
            }

            var hasElse = block.Any() && block.Peek().Value.Keyword == Keywords.Else;
            if ( hasElse ) {
                SkipAndAssert( block, Keywords.Else );
                if ( Skip( block, TokenType.NewLine ) ) {
                    elseBlock = ParseElseBlock( block );
                    lastBlockWasLine = false;
                }
                else {
                    elseBlock = ParseElseLineBlock( block );
                    SkipAndAssert( block, TokenType.NewLine );
                    lastBlockWasLine = true;
                }
            }

            if ( !lastBlockWasLine ) {
                SkipAndAssert( block, Keywords.EndIf );
            }

            var conditionExpression = ExpressionParser.ParseBlock( condition, true );

            var ifBlockStatements = StatementParser.ParseBlock( ifBlock );
            Queue<IExpressionNode> elseIfConditionExpressions = null;
            Queue<List<IStatementNode>> elseIfBlockStatements = null;
            if ( elseIfcondition.Any() ) {
                elseIfConditionExpressions = elseIfcondition.Select( x => ExpressionParser.ParseBlock( x, true ) )
                    .Where( x => x != null )
                    .ToQueue();

                elseIfBlockStatements = elseIfBlock.Select( x => StatementParser.ParseBlock( x ) )
                    .ToQueue();
            }

            IEnumerable<IStatementNode> elseBlockStatements = null;
            if ( elseBlock != null ) {
                elseBlockStatements = StatementParser.ParseBlock( elseBlock );
            }

            return AutoitStatementFactory.CreateIfElseStatement(
                conditionExpression, ifBlockStatements, elseIfConditionExpressions, elseIfBlockStatements, elseBlockStatements );
        }

        private TokenCollection ParseElseLineBlock( TokenQueue block ) {
            return ParseIfLineBlock( block );
        }

        private TokenCollection ParseElseIfLineBlock( TokenQueue block ) {
            return ParseIfLineBlock( block );
        }

        private TokenCollection ParseIfLineBlock( TokenQueue block ) {
            return new TokenCollection( block.DequeueUntil( x => x.Type == TokenType.NewLine ) );
        }
    }
}
