using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class ContinueLoopStatementStrategy : StatementParserStrategyBase<ContinueLoopStatement>
    {
        public ContinueLoopStatementStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitStatementFactory autoitStatementFactory ) : base( statementParser, expressionParser, autoitStatementFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseContinueloop( block ).ToEnumerable();
        }

        private ContinueLoopStatement ParseContinueloop( TokenQueue block ) {
            Token expressionPart = block.DequeueWhile( x => x.Type != TokenType.NewLine ).SingleOrDefault();

            TokenNode level = expressionPart != null
                ? new TokenNode( expressionPart )
                : null;

            ConsumeAndEnsure( block, TokenType.NewLine );

            return AutoitStatementFactory.CreateContinueloopStatement( level );
        }
    }
}
