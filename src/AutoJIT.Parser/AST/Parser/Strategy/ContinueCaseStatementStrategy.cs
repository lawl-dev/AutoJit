using System.Collections.Generic;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ContinueCaseStatementStrategy : StatementParserStrategyBase<ContinueCaseStatement>
    {
        public ContinueCaseStatementStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseContinueCase().ToEnumerable();
        }

        private ContinueCaseStatement ParseContinueCase() {
            return AutoitSyntaxFactory.CreateContinueCaseStatement();
        }
    }
}
