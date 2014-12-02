using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Collection;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class AssignStatementParserStrategy : StatementParserStrategyBase<AssignStatement>
    {
        public AssignStatementParserStrategy( IStatementParser statementParser, IExpressionParser expressionParser, IAutoitSyntaxFactory autoitSyntaxFactory ) : base( statementParser, expressionParser, autoitSyntaxFactory ) {}

        public override IEnumerable<IStatementNode> Parse( TokenQueue block ) {
            return ParseAssignStatement( block ).ToEnumerable();
        }

        private AssignStatement ParseAssignStatement( TokenQueue block ) {
            var line = GetLine( block );

            var variableExpression = ExpressionParser.ParseSingle<VariableExpression>( line );
            
            Token assignOperator = GetAssignOperator( line );
            
            IExpressionNode variableAssignExpression = ExpressionParser.ParseBlock( new TokenCollection( line ), true );

            return AutoitSyntaxFactory.CreateAssignStatement( variableExpression, variableAssignExpression, assignOperator );
        }

        public Token GetAssignOperator(TokenQueue block)
        {
            Token token = block.Peek();
            if (!token.IsMathExpression
                 &&
                 !token.IsNumberExpression
                 &&
                 !token.IsBooleanExpression
                 &&
                 !token.IsAssignExpression)
            {
                throw new SyntaxTreeException(string.Format("Invalid Token in AssignExpression: {0}", token.Type), token.Col, token.Line);
            }
            return block.Dequeue();
        }
    }
}
