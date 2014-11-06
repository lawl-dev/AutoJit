using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class FunctionCallStatementParserStrategy : StatementParserStrategyBase<FunctionCallStatement>
    {
        public FunctionCallStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseFunctionCall( block ).ToEnumerable();
        }

        private FunctionCallStatement ParseFunctionCall( TokenQueue block ) {
            var functionCallExpression = ExpressionParser.ParseSingle<CallExpression>( block );

            return AutoitStatementFactory.CreateFunctionCallStatement( functionCallExpression );
        }
    }
}
