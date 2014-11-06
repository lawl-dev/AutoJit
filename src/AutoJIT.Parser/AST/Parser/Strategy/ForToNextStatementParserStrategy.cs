using System.Collections.Generic;
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
    public sealed class ForToNextStatementParserStrategy : StatementParserStrategyBase<ForToNextStatement>
    {
        public ForToNextStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseForTo( block ).ToEnumerable();
        }

        private ForToNextStatement ParseForTo( TokenQueue block ) {
            var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );

            ConsumeAndEnsure( block, TokenType.Equal );

            TokenCollection startExpressionTokenCollection = ParseForToStartExpression( block );
            TokenCollection endExpressionTokenCollection = ParseForToStopExpression( block );
            TokenCollection stepExpressionTokenCollection = null;

            if ( block.Peek().Value.Keyword == Keywords.Step ) {
                ConsumeAndEnsure( block, Keywords.Step );
                stepExpressionTokenCollection = ParseForToStepExpression( block );
            }
            TokenCollection statementsTokenCollection = ParseForToStatements( block );

            IExpressionNode startExpression = ExpressionParser.ParseBlock( startExpressionTokenCollection, true );
            IExpressionNode endExpression = ExpressionParser.ParseBlock( endExpressionTokenCollection, true );
            IExpressionNode stepExpressions = null;
            if ( stepExpressionTokenCollection != null ) {
                stepExpressions = ExpressionParser.ParseBlock( stepExpressionTokenCollection, true );
            }

            List<IStatementNode> statements = StatementParser.ParseBlock( statementsTokenCollection );

            return AutoitStatementFactory.CreateForToNextStatement( variableExpression, startExpression, endExpression, stepExpressions, statements );
        }
    }
}
