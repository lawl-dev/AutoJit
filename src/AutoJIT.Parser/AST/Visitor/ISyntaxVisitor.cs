using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;

namespace AutoJIT.Parser.AST.Visitor
{
    public interface ISyntaxVisitor
    {
        void Visit(ISyntaxNode node);
        void VisitArrayInitExpression(ArrayInitExpression node);
        void VisitArrayExpression(ArrayExpression node);
        void VisitBinaryExpression(BinaryExpression node);
        void VisitBooleanNegateExpression(BooleanNegateExpression node);
        void VisitCallExpression(CallExpression node);
        void VisitDefaultExpression(DefaultExpression node);
        void VisitFalseLiteralExpression(FalseLiteralExpression node);
        void VisitMacroExpression(MacroExpression node);
        void VisitNegateExpression(NegateExpression node);
        void VisitNullExpression(NullExpression node);
        void VisitNumericLiteralExpression(NumericLiteralExpression node);
        void VisitStringLiteralExpression(StringLiteralExpression node);
        void VisitTernaryExpression(TernaryExpression node);
        void VisitTrueLiteralExpression(TrueLiteralExpression node);
        void VisitUserfunctionCallExpression(UserfunctionCallExpression node);
        void VisitVariableExpression(VariableExpression node);
        void VisitContinueCaseStatement(ContinueCaseStatement node);
        void VisitContinueLoopStatement(ContinueLoopStatement node);
        void VisitDimStatement(DimStatement node);
        void VisitDoUntilStatement(DoUntilStatement node);
        void VisitExitStatement(ExitStatement node);
        void VisitForInStatement(ForInStatement node);
        void VisitForToNextStatement(ForToNextStatement node);
        void VisitFunctionCallStatement(FunctionCallStatement node);
        void VisitGlobalDeclarationStatement(GlobalDeclarationStatement node);
        void VisitIfElseStatement(IfElseStatement node);
        void VisitInitDefaultParameterStatement(InitDefaultParameterStatement node);
        void VisitLocalDeclarationStatement(LocalDeclarationStatement node);
        void VisitReDimStatement(ReDimStatement node);
        void VisitReturnStatement(ReturnStatement node);
        void VisitSelectCaseStatement(SelectCaseStatement node);
        void VisitSwitchCaseStatement(SwitchCaseStatement node);
        void VisitWhileStatement(WhileStatement node);
        void VisitFunction(Function node);
        void VisitAutoitScriptRoot(AutoitScriptRoot node);
        void VisitCaseCondition(CaseCondition node);
        void VisitFunctionExpression(FunctionExpression node);
        void VisitVariableFunctionCallExpression(VariableFunctionCallExpression node);
        void VisitAssignStatement(AssignStatement node);
        void VisitExitloopStatement(ExitloopStatement node);
        void VisitGlobalEnumDeclarationStatement(GlobalEnumDeclarationStatement node);
        void VisitLocalEnumDeclarationStatement(LocalEnumDeclarationStatement node);
        void VisitSelectCase(SelectCase node);
        void VisitSwitchCase(SwitchCase node);
        void VisitVariableFunctionCallStatement(VariableFunctionCallStatement node);
        void VisitStaticDeclarationStatement(StaticDeclarationStatement node);
        void VisitBlockStatement(BlockStatement node);
        void VisitToken(TokenNode node);
        void VisitAutoitParameter(AutoitParameter node);
    }

    public interface ISyntaxVisitor<out TResult>
    {
        TResult Visit( ISyntaxNode node );
        TResult VisitArrayInitExpression( ArrayInitExpression node );
        TResult VisitArrayExpression( ArrayExpression node );
        TResult VisitBinaryExpression( BinaryExpression node );
        TResult VisitBooleanNegateExpression( BooleanNegateExpression node );
        TResult VisitCallExpression( CallExpression node );
        TResult VisitDefaultExpression( DefaultExpression node );
        TResult VisitFalseLiteralExpression( FalseLiteralExpression node );
        TResult VisitMacroExpression( MacroExpression node );
        TResult VisitNegateExpression( NegateExpression node );
        TResult VisitNullExpression( NullExpression node );
        TResult VisitNumericLiteralExpression( NumericLiteralExpression node );
        TResult VisitStringLiteralExpression( StringLiteralExpression node );
        TResult VisitTernaryExpression( TernaryExpression node );
        TResult VisitTrueLiteralExpression( TrueLiteralExpression node );
        TResult VisitUserfunctionCallExpression( UserfunctionCallExpression node );
        TResult VisitVariableExpression( VariableExpression node );
        TResult VisitContinueCaseStatement( ContinueCaseStatement node );
        TResult VisitContinueLoopStatement( ContinueLoopStatement node );
        TResult VisitDimStatement( DimStatement node );
        TResult VisitDoUntilStatement( DoUntilStatement node );
        TResult VisitExitStatement( ExitStatement node );
        TResult VisitForInStatement( ForInStatement node );
        TResult VisitForToNextStatement( ForToNextStatement node );
        TResult VisitFunctionCallStatement( FunctionCallStatement node );
        TResult VisitGlobalDeclarationStatement( GlobalDeclarationStatement node );
        TResult VisitIfElseStatement( IfElseStatement node );
        TResult VisitInitDefaultParameterStatement( InitDefaultParameterStatement node );
        TResult VisitLocalDeclarationStatement( LocalDeclarationStatement node );
        TResult VisitReDimStatement( ReDimStatement node );
        TResult VisitReturnStatement( ReturnStatement node );
        TResult VisitSelectCaseStatement( SelectCaseStatement node );
        TResult VisitSwitchCaseStatement( SwitchCaseStatement node );
        TResult VisitWhileStatement( WhileStatement node );
        TResult VisitFunction( Function node );
        TResult VisitAutoitScriptRoot( AutoitScriptRoot node );
        TResult VisitCaseCondition( CaseCondition node );
        TResult VisitFunctionExpression( FunctionExpression node );
        TResult VisitVariableFunctionCallExpression( VariableFunctionCallExpression node );
        TResult VisitAssignStatement( AssignStatement node );
        TResult VisitExitloopStatement( ExitloopStatement node );
        TResult VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node );
        TResult VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node );
        TResult VisitSelectCase( SelectCase node );
        TResult VisitSwitchCase( SwitchCase node );
        TResult VisitVariableFunctionCallStatement( VariableFunctionCallStatement node );
        TResult VisitStaticDeclarationStatement( StaticDeclarationStatement node );
        TResult VisitBlockStatement( BlockStatement node );
        TResult VisitToken( TokenNode node );
        TResult VisitAutoitParameter( AutoitParameter node );
    }
}
