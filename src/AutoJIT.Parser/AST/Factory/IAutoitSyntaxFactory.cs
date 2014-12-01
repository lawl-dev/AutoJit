using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Factory
{
    public interface IAutoitSyntaxFactory
    {
        AssignStatement CreateAssignStatement( VariableExpression variableExpression, IExpressionNode expression, Token @operator );
        ContinueCaseStatement CreateContinueCaseStatement();
        ContinueLoopStatement CreateContinueloopStatement( TokenNode level );
        DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression );
        DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, List<IStatementNode> block );
        ExitloopStatement CreateExitloopStatement( TokenNode level );
        ExitStatement CreateExitStatement( IExpressionNode exitCode );
        ForInStatement CreateForInStatement( VariableExpression variableName, IExpressionNode toEnumerate, List<IStatementNode> block );

        ForToNextStatement CreateForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, List<IStatementNode> block );

        FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression );
        GlobalDeclarationStatement CreateGlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst );

        EnumDeclarationStatement CreateEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, IExpressionNode autoInitExpression, bool global );

        IfElseStatement CreateIfElseStatement( IExpressionNode condition, List<IStatementNode> ifBlock, List<IExpressionNode> elseIfConditions, List<List<IStatementNode>> elseIfBlocks, List<IStatementNode> elseBlock );

        LocalDeclarationStatement CreateLocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst );

        ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression );
        ReturnStatement CreateReturnStatement( IExpressionNode returnExpression );
        SelectCaseStatement CreateSelectStatement( List<SelectCase> cases, List<IStatementNode> elseStatements );
        WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block );
        IStatementNode CreateStaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression );
        ArrayExpression CreateArrayExpression( TokenNode identifierName, List<IExpressionNode> accessParameter );
        ArrayInitExpression CreateArrayInitExpression(List<IExpressionNode> toAssign);
        BinaryExpression CreateBinaryExpression(IExpressionNode left, IExpressionNode right, TokenNode @operator);
        BooleanNegateExpression CreateBooleanNegateExpression(IExpressionNode left, TokenNode @operator);
        CallExpression CreateCallExpression( TokenNode identifierName, List<IExpressionNode> parameter );
        CaseCondition CreateCaseCondition( IExpressionNode left, IExpressionNode right );
        DefaultExpression CreateDefaultExpression();
        FalseLiteralExpression CreateFalseLiteralExpression();
        FunctionExpression CreateFunctionExpression( TokenNode identifierName );
        MacroExpression CreateMacroExpression( TokenNode identifierName );
        NegateExpression CreateNegateExpression( IExpressionNode expression );
        NullExpression CreateNullExpression();
        NumericLiteralExpression CreateNumericLiteralExpression( TokenNode literalToken, List<TokenNode> signOperators );
        StringLiteralExpression CreateStringLiteralExpression( TokenNode literalToken );
        TernaryExpression CreateTernaryExpression( IExpressionNode condition, IExpressionNode ifTrue, IExpressionNode ifFalse );
        TokenNode CreateTokenNode( Token token );
        TokenNode CreateTokenNode(string value);
        TrueLiteralExpression CreateTrueLiteralExpression();
        UserfunctionCallExpression CreateUserfunctionCallExpression( TokenNode identifierName, List<IExpressionNode> parameter );
        UserfunctionExpression CreateUserfunctionExpression( TokenNode identifierName );
        VariableExpression CreateVariableExpression( TokenNode identifierName );
        VariableFunctionCallExpression CreateVariableFunctionCallExpression( VariableExpression variableExpression, List<IExpressionNode> parameter );
        TokenNode CreateTokenNode( int token );
        BlockStatement CreateBlockStatement( List<IStatementNode> statementNodes );
        SelectCase CreateSelectCase( IExpressionNode caseCondition, BlockStatement blockStatement );
        SwitchCase CreateSwitchCase( List<CaseCondition> caseConditions, BlockStatement blockStatement );
        SwitchCaseStatement CreateSwitchCaseStatement( IExpressionNode condition, List<SwitchCase> cases, BlockStatement elseBlock );
        VariableFunctionCallStatement CreateVariableFunctionCallStatement( VariableFunctionCallExpression variableFunctionCallExpression );
        Function CreateFunction( TokenNode name, List<AutoitParameter> parameter, BlockStatement functionStatements );
        AutoitScriptRoot CreateRoot( List<Function> functions, BlockStatement main, PragmaOptions pragmaOptions );
        StringLiteralExpression CreateStringLiteralExpression(string literalToken);
        AutoitParameter CreateParameter( Token name, IExpressionNode defaultExpression, bool isByRef, bool isConst );
        ValueExpression CreateValueExpression();
        PropertyGetter CreatePropertyGetter( BlockStatement statements );
        PropertySetter CreatePropertySetter( BlockStatement statements );
        PropertyDeclarationStatement CreateProperty( VariableExpression variableExpression, PropertyGetter propertyGetter, PropertySetter propertySetter );
        EmptyStatement CreateEmptyStatement();
    }
}
