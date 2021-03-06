using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Extensions;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class WhileStatementParserStrategy : StatementParserStrategyBase<WhileStatement>
    {
        public WhileStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseWhile( block ).ToEnumerable();
        }

        private WhileStatement ParseWhile( TokenQueue block ) {
            var conditionLine = GetLine( block );

            TokenCollection whileBlock = ParseWhileBlock( block );
            
            IExpressionNode whileExpression = ExpressionParser.ParseBlock(new TokenCollection( conditionLine ), true);
            List<IStatementNode> whileBlockStatements = StatementParser.ParseBlock( whileBlock );

            ConsumeNewLine(block);

            return AutoitSyntaxFactory.CreateWhileStatement( whileExpression, whileBlockStatements );
        }

        private TokenCollection ParseWhileBlock(TokenQueue block)
        {
            return GetBetween(block, Keywords.While, Keywords.Wend, true);
        }
    }
}
