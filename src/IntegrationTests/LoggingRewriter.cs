using System.Collections.Generic;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace IntegrationTests
{
    public class LoggingRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private readonly ITokenFactory _tokenFactory = new TokenFactory();

        public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
            var assignStatement = (AssignStatement)base.VisitAssignStatement( node );

            var logStatement = GetLineLogStatement( assignStatement );

            var logValueStatement = GetLineValueLogStatement( node );

            return _syntaxFactory.CreateBlockStatement(new List<IStatementNode>() { assignStatement, logStatement, logValueStatement });
        }

        private FunctionCallStatement GetLineValueLogStatement( AssignStatement node ) {
            var logFunctionName = _syntaxFactory.CreateTokenNode( "ConsoleWrite" );

            var stringLiteralExpression = _syntaxFactory.CreateStringLiteralExpression( string.Format( "New Value of {0}: ", node.Variable.ToSource() ) );

            var concatedExpression = _syntaxFactory.CreateBinaryExpression( stringLiteralExpression, (IExpressionNode) node.Variable.Clone(), _syntaxFactory.CreateTokenNode( _tokenFactory.CreateConcat( 0, 0 ) ) );

            var logExpression = _syntaxFactory.CreateCallExpression( logFunctionName, new List<IExpressionNode>() { concatedExpression } );

            return _syntaxFactory.CreateFunctionCallStatement( logExpression );
        }

        private FunctionCallStatement GetLineLogStatement( ISyntaxNode assignStatement ) {
            var logFunctionName = _syntaxFactory.CreateTokenNode( "ConsoleWrite" );

            var toLog = string.Format( "Executed assignment: {0}", assignStatement.ToSource() );

            var stringLiteralExpression = _syntaxFactory.CreateStringLiteralExpression( toLog );

            var logExpression = _syntaxFactory.CreateCallExpression( logFunctionName, new List<IExpressionNode>() { stringLiteralExpression } );

            var logStatement = _syntaxFactory.CreateFunctionCallStatement( logExpression );
            return logStatement;
        }
    }
}