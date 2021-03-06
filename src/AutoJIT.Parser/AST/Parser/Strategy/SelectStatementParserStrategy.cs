using System.Collections.Generic;
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
    public sealed class SelectStatementParserStrategy : StatementParserStrategyBase<SelectCaseStatement>
    {
        public SelectStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseSelect( block ).ToEnumerable();
        }

        private SelectCaseStatement ParseSelect( TokenQueue block ) {
            ConsumeAndEnsure( block, TokenType.NewLine );
            var cases = new List<SelectCase>();

            var elseStatements = new List<IStatementNode>();
            while ( block.Peek().Value.Keyword != Keywords.EndSelect ) {
                ConsumeAndEnsure( block, Keywords.Case );

                if ( block.Peek().Value.Keyword != Keywords.Else ) {
                    TokenCollection expression = ParseUntilNewLine( block );
                    TokenCollection caseBlock = ParseInnerUntilSwitchSelect( block );

                    IExpressionNode caseCondition = ExpressionParser.ParseBlock( expression, true );
                    List<IStatementNode> caseStatements = StatementParser.ParseBlock( caseBlock );
                    cases.Add( AutoitSyntaxFactory.CreateSelectCase( caseCondition, AutoitSyntaxFactory.CreateBlockStatement( caseStatements ) ) );
                }
                else {
                    ConsumeAndEnsure( block, Keywords.Else );
                    TokenCollection elseBlock = ParseInnerUntil( block, Keywords.Select, Keywords.EndSelect, true );

                    elseStatements = StatementParser.ParseBlock( elseBlock );
                }
            }
            ConsumeAndEnsure( block, Keywords.EndSelect );
            return AutoitSyntaxFactory.CreateSelectStatement( cases, elseStatements );
        }
    }
}
