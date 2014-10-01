using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class AssignStatementParserStrategy : StatementParserStrategyBase<AssignStatement>
    {
        public AssignStatementParserStrategy(
            IStatementParser statementParser,
            IExpressionParser expressionParser,
            IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseAssignStatement( block ).ToEnumerable();
        }

        private AssignStatement ParseAssignStatement( TokenQueue block ) {
            var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( block );
            var assignOperator = ParseVariableAssign( block );
            var variableAssignExpressionTokenCollection = ParseVariableAssignExpression( block );

            var variableAssignExpression = ExpressionParser.ParseBlock( variableAssignExpressionTokenCollection, true );

            return AutoitStatementFactory.CreateAssignStatement( variableExpression, variableAssignExpression, assignOperator );
        }
    }
}
