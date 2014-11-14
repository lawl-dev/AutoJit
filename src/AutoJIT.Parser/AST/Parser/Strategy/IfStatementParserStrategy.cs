using System.Collections.Generic;
using System.Linq;
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
    public sealed class IfStatementParserStrategy : StatementParserStrategyBase<IfElseStatement>
    {
        public IfStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

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
            if ( Consume( block, TokenType.NewLine ) ) {
                ifBlock = ParseIfBlock( block );
            }
            else {
                ifBlock = ParseIfLineBlock( block );
                lastBlockWasLine = true;
            }

            bool hasElseIf = block.Any() && block.Peek().Value.Keyword == Keywords.ElseIf;
            while ( hasElseIf ) {
                ConsumeAndEnsure( block, Keywords.ElseIf );
                TokenCollection elseIfCondition = ParseElseIfCondition( block );
                elseIfcondition.Add( elseIfCondition );

                TokenCollection elseIfBlock2;
                if ( Consume( block, TokenType.NewLine ) ) {
                    elseIfBlock2 = ParseElseIfBlock( block );
                    lastBlockWasLine = false;
                }
                else {
                    elseIfBlock2 = ParseElseIfLineBlock( block );
                    ConsumeAndEnsure( block, TokenType.NewLine );
                    lastBlockWasLine = true;
                }

                elseIfBlock.Add( elseIfBlock2 );
                hasElseIf = block.Any() && block.Peek().Value.Keyword == Keywords.ElseIf;
            }

            bool hasElse = block.Any() && block.Peek().Value.Keyword == Keywords.Else;
            if ( hasElse ) {
                ConsumeAndEnsure( block, Keywords.Else );
                if ( Consume( block, TokenType.NewLine ) ) {
                    elseBlock = ParseElseBlock( block );
                    lastBlockWasLine = false;
                }
                else {
                    elseBlock = ParseElseLineBlock( block );
                    ConsumeAndEnsure( block, TokenType.NewLine );
                    lastBlockWasLine = true;
                }
            }

            if ( !lastBlockWasLine ) {
                ConsumeAndEnsure( block, Keywords.EndIf );
            }

            IExpressionNode conditionExpression = ExpressionParser.ParseBlock( condition, true );

            List<IStatementNode> ifBlockStatements = StatementParser.ParseBlock( ifBlock );
            List<IExpressionNode> elseIfConditionExpressions = new List<IExpressionNode>();
            List<List<IStatementNode>> elseIfBlockStatements = new List<List<IStatementNode>>();
            if ( elseIfcondition.Any() ) {
                elseIfConditionExpressions = elseIfcondition.Select( x => ExpressionParser.ParseBlock( x, true ) ).Where( x => x != null ).ToList();

                elseIfBlockStatements = elseIfBlock.Select( x => StatementParser.ParseBlock( x ) ).ToList();
            }

            List<IStatementNode> elseBlockStatements = new List<IStatementNode>();
            if ( elseBlock != null ) {
                elseBlockStatements = StatementParser.ParseBlock( elseBlock );
            }

            return AutoitSyntaxFactory.CreateIfElseStatement( conditionExpression, ifBlockStatements, elseIfConditionExpressions, elseIfBlockStatements, elseBlockStatements );
        }

        private TokenCollection ParseElseLineBlock( TokenQueue block ) {
            return ParseIfLineBlock( block );
        }

        private TokenCollection ParseElseIfLineBlock( TokenQueue block ) {
            return ParseIfLineBlock( block );
        }

        private TokenCollection ParseIfLineBlock( TokenQueue block ) {
            return new TokenCollection( block.DequeueUntil( x => x.Type == TokenType.NewLine, false) );
        }
    }
}
