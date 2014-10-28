using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ExitLoopStatementParserStrategy : StatementParserStrategyBase<ExitloopStatement>
    {
        public ExitLoopStatementParserStrategy(
        IStatementParser statementParser,
        IExpressionParser expressionParser,
        IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseExitloop( block ).ToEnumerable();
        }

        private ExitloopStatement ParseExitloop( TokenQueue block ) {
            Token expressionPart = block.DequeueWhile( x => x.Type != TokenType.NewLine ).SingleOrDefault();

            int level = expressionPart != null
            ? expressionPart.Value.Int32Value
            : 1;

            ConsumeAndEnsure( block, TokenType.NewLine );

            return AutoitStatementFactory.CreateExitloopStatement( level );
        }
    }
}
