using System.Collections.Generic;
using System.Linq;
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

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class SwitchCaseStatementParserStrategy : StatementParserStrategyBase<SwitchCaseStatement>
    {
        public SwitchCaseStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseSwitch( block ).ToEnumerable();
        }

        private SwitchCaseStatement ParseSwitch( TokenQueue block ) {
            var expression = GetLine( block );

            IExpressionNode condition = ExpressionParser.ParseBlock( new TokenCollection( expression ), true );
            
            var cases = new List<SwitchCase>();

            List<IStatementNode> @else = null;

            ConsumeAndEnsure( block, Keywords.Case );

            while ( block.Peek().Value.Keyword != Keywords.EndSwitch ) {
                var line = GetLine( block );

                var conditions = new List<TokenCollection>();
                do {
                    List<Token> conditionToken = ExtractUntilNextDeclaration( line );
                    conditions.Add( new TokenCollection( conditionToken ) );
                } while ( line.Any()
                          &&
                          Consume( line, TokenType.Comma ) );

                TokenCollection caseBlock = ParseInnerUntilSwitchSelect( block );

                if ( conditions.Count == 1
                     &&
                     conditions[0].Count == 1
                     &&
                     conditions[0][0].Value.Keyword == Keywords.Else ) {
                    @else = StatementParser.ParseBlock( caseBlock );
                }
                else {
                    List<CaseCondition> conditionExpressions = conditions.Select( ParseCaseCondition ).ToList();
                    cases.Add( AutoitSyntaxFactory.CreateSwitchCase( conditionExpressions, AutoitSyntaxFactory.CreateBlockStatement( StatementParser.ParseBlock( caseBlock ) ) ) );
                }

                if ( block.Peek().Value.Keyword != Keywords.EndSwitch ) {
                    ConsumeAndEnsure( block, Keywords.Case );
                }
            }
            ConsumeAndEnsure( block, Keywords.EndSwitch );
            return AutoitSyntaxFactory.CreateSwitchCaseStatement(
                condition, cases, @else == null
                    ? null
                    : AutoitSyntaxFactory.CreateBlockStatement( @else ) );
        }

        private CaseCondition ParseCaseCondition( TokenCollection line ) {
            IExpressionNode left = null;
            IExpressionNode right = null;
            if ( line.Any( x => x.Value.Keyword == Keywords.To ) ) {
                List<List<Token>> parts = line.Split( x => x.Value.Keyword == Keywords.To );
                if ( parts.Count != 2 ) {
                    throw new SyntaxTreeException( "Unexpected To in statement", parts[0][0].Col, parts[0][0].Line );
                }
                left = ExpressionParser.ParseBlock( new TokenCollection( parts.First() ), true );
                right = ExpressionParser.ParseBlock( new TokenCollection( parts.Skip( 1 ).First() ), true );
            }
            else if ( line.Any()
                      &&
                      line[0].Value.Keyword != Keywords.Else ) {
                left = ExpressionParser.ParseBlock( line, true );
            }
            else {
                if ( line[0].Value.Keyword != Keywords.Else ) {
                    throw new SyntaxTreeException( "Unexpected token", line[0].Col, line[0].Line );
                }
            }
            return AutoitSyntaxFactory.CreateCaseCondition( left, right );
        }
    }
}
