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
        public ContinueLoopStatementStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseContinueloop( block ).ToEnumerable();
        }

        private ContinueLoopStatement ParseContinueloop( TokenQueue block ) {
            var line = GetLine( block );
            
            Ensure( () => line.Count == 0 || line.Count == 1 );

            TokenNode level = line.Count == 1
                ? new TokenNode( line.Single() )
                : AutoitSyntaxFactory.CreateTokenNode( 1 );
            

            return AutoitSyntaxFactory.CreateContinueloopStatement( level );
        }
    }
}
