using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class AutoitLoggingVisitorWrapper
    {
        private readonly IScriptParser _scriptParser;

        public AutoitLoggingVisitorWrapper() {
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

    public class AutoitSimpleVariableObfuscator
    {
        private readonly IScriptParser _scriptParser;

        public AutoitSimpleVariableObfuscator()
        {
            var parserBootStrapper = new ParserBootStrapper();
            _scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
        }
        public string Process(string autoitCode)
        {
            AutoitScriptRoot autoitScriptRoot = _scriptParser.ParseScript(autoitCode, new PragmaOptions());
            var rewriter = new VariableObfuscatorRewriter();
            ISyntaxNode newTree = rewriter.Visit(autoitScriptRoot);
            return newTree.ToSource();
        }
    }

    public class VariableObfuscatorRewriter : SyntaxRewriterBase
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

    public class AutoitFunctionObfuscator
    {
        private readonly IScriptParser _scriptParser;

        public AutoitFunctionObfuscator()
        {
            var parserBootStrapper = new ParserBootStrapper();
            _scriptParser = parserBootStrapper.GetInstance<IScriptParser>();
        }
        public string Process(string autoitCode)
        {
            AutoitScriptRoot autoitScriptRoot = _scriptParser.ParseScript(autoitCode, new PragmaOptions());
            var rewriter = new FunctionObfuscatorRewriter();
            ISyntaxNode newTree = rewriter.Visit(autoitScriptRoot);
            return newTree.ToSource();
        }
    }

    public class FunctionObfuscatorRewriter : SyntaxRewriterBase
    {
        private readonly IAutoitSyntaxFactory _syntaxFactory = new AutoitSyntaxFactory( new TokenFactory() );
        private readonly ITokenFactory _tokenFactory = new TokenFactory();
        private readonly List<Function> _newFunctions = new List<Function>();

        public override ISyntaxNode VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            var root = (AutoitScriptRoot)base.VisitAutoitScriptRoot( node );
            
            var functions = new List<Function>();
            functions.AddRange( root.Functions );
            functions.AddRange( _newFunctions );
            return _syntaxFactory.CreateRoot( functions.Select( x => (Function) x.Clone() ).ToList(), (BlockStatement) root.MainFunction.Clone(), root.PragmaOptions );
        }

        public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
            var assignStatement = (AssignStatement)base.VisitAssignStatement( node );

            var parameterNames = GetParameterNames( assignStatement.ExpressionToAssign );

            var parameters = parameterNames.Select( x=>_syntaxFactory.CreateParameter( _tokenFactory.CreateVariable( x, 0 ,0 ), null, false, false ) ).ToList();

            var returnStatement = _syntaxFactory.CreateReturnStatement( (IExpressionNode) assignStatement.ExpressionToAssign.Clone() );

            var newFunctionName = string.Format( "_f{0}", Guid.NewGuid().ToString("N") );

            var function = _syntaxFactory.CreateFunction( _syntaxFactory.CreateTokenNode( newFunctionName ), parameters, new List<IStatementNode>(){returnStatement} );

            _newFunctions.Add( function );

            var functionCallExpression = _syntaxFactory.CreateUserfunctionCallExpression( _syntaxFactory.CreateTokenNode( newFunctionName ), parameterNames.Select( x => _syntaxFactory.CreateVariableExpression( _syntaxFactory.CreateTokenNode( _tokenFactory.CreateVariable( x, 0, 0 ) ) ) ).OfType<IExpressionNode>().ToList() );


            return assignStatement.Update( assignStatement.Variable, functionCallExpression, assignStatement.Operator  );
        }


        private List<string> GetParameterNames(ISyntaxNode root) {
            return new NeededVariableVisitor().GetUsedVariables( root );
        }

        class NeededVariableVisitor : SyntaxRewriterBase
        {
            private readonly List<string> _usedVariables = new List<string>();

            public List<string> GetUsedVariables( ISyntaxNode root ) {
                Visit( root );
                return _usedVariables;
            }

            public override ISyntaxNode VisitVariableExpression( VariableExpression node ) {
                var expression = base.VisitVariableExpression( node );
                if ( !_usedVariables.Contains( node.IdentifierName.Token.Value.StringValue ) ) {
                    _usedVariables.Add(node.IdentifierName.Token.Value.StringValue);
                }
                return expression;
            }

            public override ISyntaxNode VisitArrayExpression( ArrayExpression node ) {
                var expression = base.VisitArrayExpression(node);
                if (!_usedVariables.Contains(node.IdentifierName.Token.Value.StringValue))
                {
                    _usedVariables.Add(node.IdentifierName.Token.Value.StringValue);
                }
                return expression;
            }
        }
    }
}