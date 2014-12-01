using System.Collections.Generic;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class EmptyStatementParserStrategy : StatementParserStrategyBase<EmptyStatement>
    {
        public EmptyStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}
        
        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            ConsumeAndEnsure( block, TokenType.NewLine );
            return AutoitSyntaxFactory.CreateEmptyStatement().ToEnumerable();
        }
    }
}