using System;
using System.Collections.Generic;
using System.IO;
using AutoJIT.Parser;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.AST.Visitor;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace IntegrationTests
{
    public class AutoitLoggingWriter
    {
        private readonly IScriptParser _scriptParser;

        public AutoitLoggingWriter() {
            var parserBootStrapper = new ParserBootStrapper();
            _scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
        }
        public string Process( string autoitCode ) {
            AutoitScriptRoot autoitScriptRoot = _scriptParser.ParseScript(autoitCode, new PragmaOptions());
            var rewriter = new LoggingRewriter();
            ISyntaxNode newTree = rewriter.Visit(autoitScriptRoot);
            return newTree.ToSource();
        }
    }

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

    public class AutoitSimpleObfuscator
    {
        private readonly IScriptParser _scriptParser;

        public AutoitSimpleObfuscator()
        {
            var parserBootStrapper = new ParserBootStrapper();
            _scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
        }
        public string Process(string autoitCode)
        {
            AutoitScriptRoot autoitScriptRoot = _scriptParser.ParseScript(autoitCode, new PragmaOptions());
            var rewriter = new ObfuscatorRewriter();
            ISyntaxNode newTree = rewriter.Visit(autoitScriptRoot);
            return newTree.ToSource();
        }
    }

    public class ObfuscatorRewriter : SyntaxRewriterBase
    {
        private readonly Dictionary<string, string> _variableNames = new Dictionary<string, string>();
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private readonly ITokenFactory _tokenFactory = new TokenFactory();

        public override ISyntaxNode VisitToken( TokenNode node ) {
            if ( !( node.Parent is VariableExpression || node.Parent is AutoitParameter ) ) {
                return base.VisitToken( node );
            }

            var variableName = node.Token.Value.StringValue;

            if ( !_variableNames.ContainsKey( variableName ) ) {
                _variableNames.Add( variableName, Guid.NewGuid().ToString( "N" ) );
            }


            return _syntaxFactory.CreateTokenNode( _tokenFactory.CreateVariable( _variableNames[variableName], 0, 0 ) );
        }
    }
}