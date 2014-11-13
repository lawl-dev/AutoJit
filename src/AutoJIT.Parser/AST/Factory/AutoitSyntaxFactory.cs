using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.AST.Factory
{
    public sealed class AutoitSyntaxFactory : IAutoitSyntaxFactory
    {
        private readonly ITokenFactory _tokenFactory;

        public AutoitSyntaxFactory(ITokenFactory tokenFactory) {
            _tokenFactory = tokenFactory;
        }

        public AssignStatement CreateAssignStatement( VariableExpression variableExpression, IExpressionNode expression, Token @operator ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if ( expression == null ) {
                throw new ArgumentNullException( "expression" );
            }

            if ( @operator == null ) {
                throw new ArgumentNullException( "operator" );
            }

            if ( @operator.Type != TokenType.PowAssign
                 &&
                 @operator.Type != TokenType.ConcatAssign
                 &&
                 @operator.Type != TokenType.DivAssign
                 &&
                 @operator.Type != TokenType.MinusAssign
                 &&
                 @operator.Type != TokenType.MultAssign
                 &&
                 @operator.Type != TokenType.PlusAssign
                 &&
                 @operator.Type != TokenType.Equal ) {
                throw new ArgumentException( "operator" );
            }

            return new AssignStatement( variableExpression, expression, new TokenNode( @operator ) );
        }

        public ContinueCaseStatement CreateContinueCaseStatement() {
            return new ContinueCaseStatement();
        }

        public ContinueLoopStatement CreateContinueloopStatement( TokenNode level ) {
            return new ContinueLoopStatement( level );
        }

        public DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new DimStatement( variableExpression, initExpression );
        }

        public DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, IEnumerable<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new DoUntilStatement( condition, new BlockStatement( block ) );
        }

        public ExitloopStatement CreateExitloopStatement( TokenNode level ) {
            return new ExitloopStatement( level );
        }

        public ExitStatement CreateExitStatement( IExpressionNode exitCode ) {
            return new ExitStatement( exitCode );
        }

        public ForInStatement CreateForInStatement( VariableExpression variableName, IExpressionNode toEnumerate, IEnumerable<IStatementNode> block ) {
            if ( variableName == null ) {
                throw new ArgumentNullException( "variableName" );
            }

            if ( toEnumerate == null ) {
                throw new ArgumentNullException( "toEnumerate" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForInStatement( variableName, toEnumerate, new BlockStatement( block ) );
        }

        public ForToNextStatement CreateForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, IEnumerable<IStatementNode> block ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            if ( startExpression == null ) {
                throw new ArgumentNullException( "startExpression" );
            }

            if ( endExpression == null ) {
                throw new ArgumentNullException( "endExpression" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            return new ForToNextStatement( variableExpression, startExpression, endExpression, stepExpression, new BlockStatement( block ) );
        }

        public FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression ) {
            if ( functionCallExpression == null ) {
                throw new ArgumentNullException( "functionCallExpression" );
            }

            return new FunctionCallStatement( functionCallExpression );
        }

        public GlobalDeclarationStatement CreateGlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new GlobalDeclarationStatement( variableExpression, initExpression, isConst );
        }

        public EnumDeclarationStatement CreateEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, IExpressionNode autoInitExpression, bool global ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            if ( global ) {
                return new GlobalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
            }
            return new LocalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
        }

        public IfElseStatement CreateIfElseStatement( IExpressionNode condition, IEnumerable<IStatementNode> ifBlock, IEnumerable<IExpressionNode> elseIfConditions, IEnumerable<IEnumerable<IStatementNode>> elseIfBlocks, IEnumerable<IStatementNode> elseBlock ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( ifBlock == null ) {
                throw new ArgumentNullException( "ifBlock" );
            }

            return new IfElseStatement( condition, new BlockStatement( ifBlock ), elseIfConditions, elseIfBlocks.Select( x => new BlockStatement( x ) ), new BlockStatement( elseBlock ) );
        }

        public LocalDeclarationStatement CreateLocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            return new LocalDeclarationStatement( variableExpression, initExpression, isConst );
        }

        public ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression ) {
            if ( arrayExpression == null ) {
                throw new ArgumentNullException( "arrayExpression" );
            }

            return new ReDimStatement( arrayExpression );
        }

        public ReturnStatement CreateReturnStatement( IExpressionNode returnExpression ) {
            if ( returnExpression == null ) {
                throw new ArgumentNullException( "returnExpression" );
            }

            return new ReturnStatement( returnExpression );
        }

        public SelectCaseStatement CreateSelectStatement( IEnumerable<SelectCase> cases, IEnumerable<IStatementNode> elseStatements ) {
            if ( cases == null ) {
                throw new ArgumentNullException( "cases" );
            }

            return new SelectCaseStatement(
                cases, elseStatements != null && elseStatements.Any()
                    ? new BlockStatement( elseStatements )
                    : null );
        }

        public WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            return new WhileStatement( condition, new BlockStatement( block ) );
        }

        public IStatementNode CreateStaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            return new StaticDeclarationStatement( variableExpression, initExpression );
        }

        public ArrayExpression CreateArrayExpression( TokenNode identifierName, List<IExpressionNode> accessParameter ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            if ( accessParameter == null ) {
                throw new ArgumentNullException("accessParameter");
            }
            
            if ( !accessParameter.Any() ) {
                throw new ArgumentException("Not allowed", "accessParameter");
            }

            return new ArrayExpression( identifierName, accessParameter );
        }

        public ArrayInitExpression CreateArrayInitExpression(List<IExpressionNode> toAssign) {
            if ( toAssign == null ) {
                throw new ArgumentNullException("toAssign");
            }
            return new ArrayInitExpression( toAssign );
        }

        public BinaryExpression CreateBinaryExpression(IExpressionNode left, IExpressionNode right, TokenNode @operator) {
            if ( left == null ) {
                throw new ArgumentNullException("Left");
            }

            if ( right == null ) {
                throw new ArgumentNullException("right");
            }

            if ( @operator == null ) {
                throw new ArgumentNullException("operator");
            }

            if ( !@operator.Token.IsBinaryExpression ) {
                throw new ArgumentException("Invalid operator", "@operator");
            }

            return new BinaryExpression( left, right, @operator );
        }

        public BooleanNegateExpression CreateBooleanNegateExpression(IExpressionNode left, TokenNode @operator) {
            if ( left == null ) {
                throw new ArgumentNullException("left");
            }

            if ( @operator == null ) {
                throw new ArgumentNullException("operator");
            }

            if ( @operator.Token.Type != TokenType.NOT ) {
                throw new ArgumentException("Invalid token", "@operator");
            }

            return new BooleanNegateExpression( left, @operator );
        }

        public CallExpression CreateCallExpression( TokenNode identifierName, List<IExpressionNode> parameter ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            if ( parameter == null ) {
                throw new ArgumentNullException("parameter");
            }

            return new CallExpression( identifierName, parameter );
        }

        public CaseCondition CreateCaseCondition( IExpressionNode left, IExpressionNode right ) {
            if ( left == null ) {
                throw new ArgumentNullException("left");
            }

            if ( right == null ) {
                throw new ArgumentNullException("right");
            }

            return new CaseCondition( left, right);
        }

        public DefaultExpression CreateDefaultExpression() {
            return new DefaultExpression();
        }

        public FalseLiteralExpression CreateFalseLiteralExpression() {
            return new FalseLiteralExpression();
        }

        public FunctionExpression CreateFunctionExpression( TokenNode identifierName ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }
            
            return new FunctionExpression( identifierName );
        }

        public MacroExpression CreateMacroExpression( TokenNode identifierName ) {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }
            
            return new MacroExpression( identifierName );
        }

        public NegateExpression CreateNegateExpression( IExpressionNode expression ) {
            if ( expression == null ) {
                throw new ArgumentNullException("expression");
            }

            return new NegateExpression( expression );
        }

        public NullExpression CreateNullExpression() {
            return new NullExpression();
        }

        public NumericLiteralExpression CreateNumericLiteralExpression( TokenNode literalToken, IEnumerable<TokenNode> signOperators ) {
            if ( literalToken == null ) {
                throw new ArgumentNullException("literalToken");
            }

            if ( signOperators == null ) {
                throw new ArgumentNullException("signOperators");
            }

            return new NumericLiteralExpression( literalToken, signOperators );
        }

        public StringLiteralExpression CreateStringLiteralExpression( TokenNode literalToken ) {
            if ( literalToken == null ) {
                throw new ArgumentNullException("literalToken");
            }

            if ( literalToken.Token.Type != TokenType.String ) {
                throw new ArgumentException( "Literal is not a string", "literalToken" );
            }
            return new StringLiteralExpression( literalToken );
        }

        public TernaryExpression CreateTernaryExpression( IExpressionNode condition, IExpressionNode ifTrue, IExpressionNode ifFalse ) {
            if ( condition == null ) {
                throw new ArgumentNullException("condition");
            }

            if ( ifTrue == null ) {
                throw new ArgumentNullException("ifTrue");
            }

            if ( ifFalse == null ) {
                throw new ArgumentNullException("ifFalse");
            }

            return new TernaryExpression( condition, ifTrue, ifFalse );
        }

        public TokenNode CreateTokenNode( Token token ) {
            if ( token == null ) {
                throw new ArgumentNullException("token");
            }

            return new TokenNode( token );
        }

        public TokenNode CreateTokenNode(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return new TokenNode( _tokenFactory.CreateString( value, -1, -1 ) );
        }

        public TrueLiteralExpression CreateTrueLiteralExpression() {
            return new TrueLiteralExpression();
        }

        public UserfunctionCallExpression CreateUserfunctionCallExpression( TokenNode identifierName, IEnumerable<IExpressionNode> parameter ) {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }


            return new UserfunctionCallExpression( identifierName, parameter );
        }

        public UserfunctionExpression CreateUserfunctionExpression( TokenNode identifierName ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            return new UserfunctionExpression( identifierName );
        }

        public VariableExpression CreateVariableExpression( TokenNode identifierName ) {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }
            
            return new VariableExpression( identifierName );
        }

        public VariableFunctionCallExpression CreateVariableFunctionCallExpression( VariableExpression variableExpression, IEnumerable<IExpressionNode> parameter ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException("variableExpression");
            }

            if ( parameter == null ) {
                throw new ArgumentNullException("parameter");
            }

            return new VariableFunctionCallExpression( variableExpression, parameter );
        }

        public TokenNode CreateTokenNode( int token ) {
            return new TokenNode(_tokenFactory.CreateInt( token, -1, -1 ));
        }
    }
}
