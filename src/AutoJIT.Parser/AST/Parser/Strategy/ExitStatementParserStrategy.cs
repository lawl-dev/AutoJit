using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ExitStatementParserStrategy : StatementParserStrategyBase<ExitStatement>
    {
        public ExitStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseExit( block ).ToEnumerable();
        }

        private ExitStatement ParseExit( TokenQueue block ) {
            TokenCollection exitExpression = ParseUntilNewLine( block );

            IExpressionNode expressionNode = null;
            if ( exitExpression.Any() ) {
                expressionNode = ExpressionParser.ParseBlock( exitExpression, true );
            }

            return AutoitSyntaxFactory.CreateExitStatement( expressionNode );
        }
    }
}
