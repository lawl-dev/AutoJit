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

            var statement = new AssignStatement( variableExpression, expression, new TokenNode( @operator ) );
            statement.Initialize();
            return statement;
        }

        public ContinueCaseStatement CreateContinueCaseStatement() {
            var statement = new ContinueCaseStatement();
            statement.Initialize();
            return statement;
        }

        public ContinueLoopStatement CreateContinueloopStatement( TokenNode level ) {
            var statement = new ContinueLoopStatement( level );
            statement.Initialize();
            return statement;
        }

        public DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            var statement = new DimStatement( variableExpression, initExpression );
            statement.Initialize();
            return statement;
        }

        public DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            var statement = new DoUntilStatement( condition, CreateBlockStatement( block ) );
            statement.Initialize();
            return statement;
        }

        public BlockStatement CreateBlockStatement(List<IStatementNode> block)
        {
            var statement = new BlockStatement( block.ToList() );
            statement.Initialize();
            return statement;
        }

        public SelectCase CreateSelectCase( IExpressionNode caseCondition, BlockStatement blockStatement ) {
            if ( caseCondition == null ) {
                throw new ArgumentNullException("caseCondition");
            }

            if ( blockStatement == null ) {
                throw new ArgumentNullException("blockStatement");
            }

            var selectCase = new SelectCase( caseCondition, blockStatement );
            selectCase.Initialize();
            return selectCase;
        }

        public SwitchCase CreateSwitchCase( List<CaseCondition> caseConditions, BlockStatement blockStatement ) {
            if ( caseConditions == null ) {
                throw new ArgumentNullException("caseConditions");
            }

            if ( blockStatement == null ) {
                throw new ArgumentNullException("blockStatement");
            }

            var switchCase = new SwitchCase( caseConditions, blockStatement );
            switchCase.Initialize();
            return switchCase;
        }

        public SwitchCaseStatement CreateSwitchCaseStatement( IExpressionNode condition, List<SwitchCase> cases, BlockStatement elseBlock ) {
            if ( condition == null ) {
                throw new ArgumentNullException("condition");
            }

            if ( cases == null ) {
                throw new ArgumentNullException("cases");
            }


            var statement = new SwitchCaseStatement( condition, cases, elseBlock );
            statement.Initialize();
            return statement;
        }

        public VariableFunctionCallStatement CreateVariableFunctionCallStatement( VariableFunctionCallExpression variableFunctionCallExpression ) {
            if ( variableFunctionCallExpression == null ) {
                throw new ArgumentNullException("variableFunctionCallExpression");
            }

            var statement = new VariableFunctionCallStatement( variableFunctionCallExpression );
            statement.Initialize();
            return statement;
        }


        public Function CreateFunction( TokenNode name, List<AutoitParameterInfo> parameter, List<IStatementNode> functionStatements ) {
            return new Function( name, parameter, functionStatements );
        }

        public ExitloopStatement CreateExitloopStatement( TokenNode level ) {
            var statement = new ExitloopStatement( level );
            statement.Initialize();
            return statement;
        }

        public ExitStatement CreateExitStatement( IExpressionNode exitCode ) {
            var statement = new ExitStatement( exitCode );
            statement.Initialize();
            return statement;
        }

        public ForInStatement CreateForInStatement(VariableExpression variableName, IExpressionNode toEnumerate, List<IStatementNode> block)
        {
            if ( variableName == null ) {
                throw new ArgumentNullException( "variableName" );
            }

            if ( toEnumerate == null ) {
                throw new ArgumentNullException( "toEnumerate" );
            }

            if ( block == null ) {
                throw new ArgumentNullException( "block" );
            }

            var statement = new ForInStatement( variableName, toEnumerate, CreateBlockStatement( block ) );
            statement.Initialize();
            return statement;
        }

        public ForToNextStatement CreateForToNextStatement(VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, List<IStatementNode> block)
        {
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

            var statement = new ForToNextStatement( variableExpression, startExpression, endExpression, stepExpression, CreateBlockStatement( block ) );
            statement.Initialize();
            return statement;
        }

        public FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression ) {
            if ( functionCallExpression == null ) {
                throw new ArgumentNullException( "functionCallExpression" );
            }

            var statement = new FunctionCallStatement( functionCallExpression );
            statement.Initialize();
            return statement;
        }

        public GlobalDeclarationStatement CreateGlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            var statement = new GlobalDeclarationStatement( variableExpression, initExpression, isConst );
            statement.Initialize();
            return statement;
        }

        public EnumDeclarationStatement CreateEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, IExpressionNode autoInitExpression, bool global ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            if ( global ) {
                var statement = new GlobalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
                statement.Initialize();
                return statement;
            }
            var localStatementz = new LocalEnumDeclarationStatement( variableExpression, initExpression, autoInitExpression );
            localStatementz.Initialize();
            return localStatementz;
        }

        public IfElseStatement CreateIfElseStatement( IExpressionNode condition, List<IStatementNode> ifBlock, List<IExpressionNode> elseIfConditions, List<List<IStatementNode>> elseIfBlocks, List<IStatementNode> elseBlock ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            if ( ifBlock == null ) {
                throw new ArgumentNullException( "ifBlock" );
            }

            var statement = new IfElseStatement( condition, CreateBlockStatement( ifBlock ), elseIfConditions, elseIfBlocks.Select( CreateBlockStatement ).ToList(), CreateBlockStatement( elseBlock ) );
            statement.Initialize();
            return statement;
        }

        public LocalDeclarationStatement CreateLocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }

            var statement = new LocalDeclarationStatement( variableExpression, initExpression, isConst );
            statement.Initialize();
            return statement;
        }

        public ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression ) {
            if ( arrayExpression == null ) {
                throw new ArgumentNullException( "arrayExpression" );
            }

            var reDimStatement = new ReDimStatement( arrayExpression );
            reDimStatement.Initialize();
            return reDimStatement;
        }

        public ReturnStatement CreateReturnStatement( IExpressionNode returnExpression ) {
            if ( returnExpression == null ) {
                throw new ArgumentNullException( "returnExpression" );
            }

            var statement = new ReturnStatement( returnExpression );
            statement.Initialize();
            return statement;
        }

        public SelectCaseStatement CreateSelectStatement(List<SelectCase> cases, List<IStatementNode> elseStatements)
        {
            if ( cases == null ) {
                throw new ArgumentNullException( "cases" );
            }

            var statement = new SelectCaseStatement(
                cases.ToList(), elseStatements != null && elseStatements.Any()
                    ? CreateBlockStatement( elseStatements )
                    : null );
            statement.Initialize();
            return statement;
        }

        public WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block ) {
            if ( condition == null ) {
                throw new ArgumentNullException( "condition" );
            }

            var statement = new WhileStatement( condition, CreateBlockStatement( block ) );
            statement.Initialize();
            return statement;
        }

        public IStatementNode CreateStaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            if ( variableExpression == null ) {
                throw new ArgumentNullException( "variableExpression" );
            }
            var statement = new StaticDeclarationStatement( variableExpression, initExpression );
            statement.Initialize();
            return statement;
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

            var arrayExpression = new ArrayExpression( identifierName, accessParameter );
            arrayExpression.Initialize();
            return arrayExpression;
        }

        public ArrayInitExpression CreateArrayInitExpression(List<IExpressionNode> toAssign) {
            if ( toAssign == null ) {
                throw new ArgumentNullException("toAssign");
            }
            var initExpression = new ArrayInitExpression( toAssign );
            initExpression.Initialize();
            return initExpression;
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

            var expression = new BinaryExpression( left, right, @operator );
            expression.Initialize();
            return expression;
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

            var expression = new BooleanNegateExpression( left, @operator );
            expression.Initialize();
            return expression;
        }

        public CallExpression CreateCallExpression( TokenNode identifierName, List<IExpressionNode> parameter ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            if ( parameter == null ) {
                throw new ArgumentNullException("parameter");
            }

            var expression = new CallExpression( identifierName, parameter );
            expression.Initialize();
            return expression;
        }

        public CaseCondition CreateCaseCondition( IExpressionNode left, IExpressionNode right ) {
            if ( left == null ) {
                throw new ArgumentNullException("left");
            }

            var condition = new CaseCondition( left, right);
            condition.Initialize();
            return condition;
        }

        public DefaultExpression CreateDefaultExpression() {
            var expression = new DefaultExpression();
            expression.Initialize();
            return expression;
        }

        public FalseLiteralExpression CreateFalseLiteralExpression() {
            var expression = new FalseLiteralExpression();
            expression.Initialize();
            return expression;
        }

        public FunctionExpression CreateFunctionExpression( TokenNode identifierName ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            var expression = new FunctionExpression( identifierName );
            expression.Initialize();
            return expression;
        }

        public MacroExpression CreateMacroExpression( TokenNode identifierName ) {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }

            var expression = new MacroExpression( identifierName );
            expression.Initialize();
            return expression;
        }

        public NegateExpression CreateNegateExpression( IExpressionNode expression ) {
            if ( expression == null ) {
                throw new ArgumentNullException("expression");
            }

            var negateExpression = new NegateExpression( expression );
            negateExpression.Initialize();
            return negateExpression;
        }

        public NullExpression CreateNullExpression() {
            var expression = new NullExpression();
            expression.Initialize();
            return expression;
        }

        public NumericLiteralExpression CreateNumericLiteralExpression( TokenNode literalToken, List<TokenNode> signOperators ) {
            if ( literalToken == null ) {
                throw new ArgumentNullException("literalToken");
            }

            if ( signOperators == null ) {
                throw new ArgumentNullException("signOperators");
            }

            var expression = new NumericLiteralExpression( literalToken, signOperators );
            expression.Initialize();
            return expression;
        }

        public StringLiteralExpression CreateStringLiteralExpression( TokenNode literalToken ) {
            if ( literalToken == null ) {
                throw new ArgumentNullException("literalToken");
            }

            if ( literalToken.Token.Type != TokenType.String ) {
                throw new ArgumentException( "Literal is not a string", "literalToken" );
            }
            var expression = new StringLiteralExpression( literalToken );
            expression.Initialize();
            return expression;
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

            var expression = new TernaryExpression( condition, ifTrue, ifFalse );
            expression.Initialize();
            return expression;
        }

        public TokenNode CreateTokenNode( Token token ) {
            if ( token == null ) {
                throw new ArgumentNullException("token");
            }

            var tokenNode = new TokenNode( token );
            tokenNode.Initialize();
            return tokenNode;
        }

        public TokenNode CreateTokenNode(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var tokenNode = new TokenNode( _tokenFactory.CreateString( value, -1, -1 ) );
            tokenNode.Initialize();
            return tokenNode;
        }

        public TrueLiteralExpression CreateTrueLiteralExpression() {
            var literalExpression = new TrueLiteralExpression();
            literalExpression.Initialize();
            return literalExpression;
        }

        public UserfunctionCallExpression CreateUserfunctionCallExpression(TokenNode identifierName, List<IExpressionNode> parameter)
        {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            var expression = new UserfunctionCallExpression( identifierName, parameter.ToList() );
            expression.Initialize();
            return expression;
        }

        public UserfunctionExpression CreateUserfunctionExpression( TokenNode identifierName ) {
            if ( identifierName == null ) {
                throw new ArgumentNullException("identifierName");
            }

            var userfunctionExpression = new UserfunctionExpression( identifierName );
            userfunctionExpression.Initialize();
            return userfunctionExpression;
        }

        public VariableExpression CreateVariableExpression( TokenNode identifierName ) {
            if (identifierName == null)
            {
                throw new ArgumentNullException("identifierName");
            }

            var expression = new VariableExpression( identifierName );
            expression.Initialize();
            return expression;
        }

        public VariableFunctionCallExpression CreateVariableFunctionCallExpression(VariableExpression variableExpression, List<IExpressionNode> parameter)
        {
            if ( variableExpression == null ) {
                throw new ArgumentNullException("variableExpression");
            }

            if ( parameter == null ) {
                throw new ArgumentNullException("parameter");
            }

            var expression = new VariableFunctionCallExpression( variableExpression, parameter.ToList() );
            expression.Initialize();
            return expression;
        }

        public AutoitScriptRoot CreateRoot( List<Function> functions, BlockStatement main, PragmaOptions pragmaOptions ) {
            return new AutoitScriptRoot(  functions, main, pragmaOptions);
        }

        public TokenNode CreateTokenNode( int token ) {
            var tokenNode = new TokenNode(_tokenFactory.CreateInt( token, -1, -1 ));
            tokenNode.Initialize();
            return tokenNode;
        }
    }
}
