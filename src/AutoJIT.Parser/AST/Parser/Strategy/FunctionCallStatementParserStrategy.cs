using System.Collections.Generic;
using System.Linq;
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
        public FunctionCallStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseFunctionCall( block ).ToEnumerable();
        }

        private FunctionCallStatement ParseFunctionCall( TokenQueue block ) {
            var line = GetLine( block );

            var functionCallExpression = ExpressionParser.ParseSingle<CallExpression>( line );

            Ensure( () => !line.Any() );

            return AutoitSyntaxFactory.CreateFunctionCallStatement( functionCallExpression );
        }
    }
}
