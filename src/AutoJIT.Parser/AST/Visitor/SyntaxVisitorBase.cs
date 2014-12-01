using System;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;

namespace AutoJIT.Parser.AST.Visitor
{
    public abstract class SyntaxVisitorBase : ISyntaxVisitor
    {
        public virtual void Visit( ISyntaxNode node ) {
            node.Accept( this );
        }

        public virtual void VisitArrayInitExpression( ArrayInitExpression node ) {}

        public virtual void VisitArrayExpression( ArrayExpression node ) {}

        public virtual void VisitBinaryExpression( BinaryExpression node ) {}

        public virtual void VisitBooleanNegateExpression( BooleanNegateExpression node ) {}

        public virtual void VisitCallExpression( CallExpression node ) {}

        public virtual void VisitDefaultExpression( DefaultExpression node ) {}

        public virtual void VisitFalseLiteralExpression( FalseLiteralExpression node ) {}

        public virtual void VisitMacroExpression( MacroExpression node ) {}

        public virtual void VisitNegateExpression( NegateExpression node ) {}

        public virtual void VisitNullExpression( NullExpression node ) {}

        public virtual void VisitNumericLiteralExpression( NumericLiteralExpression node ) {}

        public virtual void VisitStringLiteralExpression( StringLiteralExpression node ) {}

        public virtual void VisitTernaryExpression( TernaryExpression node ) {}

        public virtual void VisitTrueLiteralExpression( TrueLiteralExpression node ) {}

        public virtual void VisitUserfunctionCallExpression( UserfunctionCallExpression node ) {}

        public virtual void VisitVariableExpression( VariableExpression node ) {}
        
        public virtual void VisitContinueCaseStatement( ContinueCaseStatement node ) {}

        public virtual void VisitContinueLoopStatement( ContinueLoopStatement node ) {}

        public virtual void VisitDimStatement( DimStatement node ) {}

        public virtual void VisitDoUntilStatement( DoUntilStatement node ) {}

        public virtual void VisitEnumDeclarationStatement( EnumDeclarationStatement node ) {}

        public virtual void VisitExitStatement( ExitStatement node ) {}

        public virtual void VisitForInStatement( ForInStatement node ) {}

        public virtual void VisitForToNextStatement( ForToNextStatement node ) {}

        public virtual void VisitFunctionCallStatement( FunctionCallStatement node ) {}

        public virtual void VisitGlobalDeclarationStatement( GlobalDeclarationStatement node ) {}

        public virtual void VisitIfElseStatement( IfElseStatement node ) {}

        public virtual void VisitInitDefaultParameterStatement( InitDefaultParameterStatement node ) {}

        public virtual void VisitLocalDeclarationStatement( LocalDeclarationStatement node ) {}

        public virtual void VisitReDimStatement( ReDimStatement node ) {}

        public virtual void VisitReturnStatement( ReturnStatement node ) {}

        public virtual void VisitSelectCaseStatement( SelectCaseStatement node ) {}

        public virtual void VisitSwitchCaseStatement( SwitchCaseStatement node ) {}

        public virtual void VisitWhileStatement( WhileStatement node ) {}

        public virtual void VisitFunction( Function node ) {}

        public virtual void VisitAutoitScriptRoot( AutoitScriptRoot node ) {}
        public virtual void VisitCaseCondition( CaseCondition node ) {}

        public virtual void VisitFunctionExpression( FunctionExpression node ) {}

        public virtual void VisitVariableFunctionCallExpression( VariableFunctionCallExpression node ) {}

        public virtual void VisitAssignStatement( AssignStatement node ) {}

        public virtual void VisitExitloopStatement( ExitloopStatement node ) {}

        public virtual void VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node ) {}

        public virtual void VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {}

        public virtual void VisitSelectCase( SelectCase node ) {}

        public virtual void VisitSwitchCase( SwitchCase node ) {}

        public virtual void VisitVariableFunctionCallStatement( VariableFunctionCallStatement node ) {}

        public virtual void VisitStaticDeclarationStatement( StaticDeclarationStatement node ) {}

        public virtual void VisitBlockStatement( BlockStatement node ) {}

        public virtual void VisitToken( TokenNode node ) {}

        public virtual void VisitAutoitParameter( AutoitParameter node ) {}
    }

    public abstract class SyntaxVisitorBase<TResult> : ISyntaxVisitor<TResult>
    {
        TResult ISyntaxVisitor<TResult>.Visit( ISyntaxNode node ) {
            return Visit( node );
        }

        public virtual TResult VisitArrayInitExpression( ArrayInitExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitArrayExpression( ArrayExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitBinaryExpression( BinaryExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitBooleanNegateExpression( BooleanNegateExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitCallExpression( CallExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitDefaultExpression( DefaultExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitFalseLiteralExpression( FalseLiteralExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitMacroExpression( MacroExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitNegateExpression( NegateExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitNullExpression( NullExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitNumericLiteralExpression( NumericLiteralExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitStringLiteralExpression( StringLiteralExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitTernaryExpression( TernaryExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitTrueLiteralExpression( TrueLiteralExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitUserfunctionCallExpression( UserfunctionCallExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitVariableExpression( VariableExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitContinueCaseStatement( ContinueCaseStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitContinueLoopStatement( ContinueLoopStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitDimStatement( DimStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitDoUntilStatement( DoUntilStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitExitStatement( ExitStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitForInStatement( ForInStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitForToNextStatement( ForToNextStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitFunctionCallStatement( FunctionCallStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitGlobalDeclarationStatement( GlobalDeclarationStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitIfElseStatement( IfElseStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitInitDefaultParameterStatement( InitDefaultParameterStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitLocalDeclarationStatement( LocalDeclarationStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitReDimStatement( ReDimStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitReturnStatement( ReturnStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitSelectCaseStatement( SelectCaseStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitSwitchCaseStatement( SwitchCaseStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitWhileStatement( WhileStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitFunction( Function node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            return VisitDefault( node );
        }

        public virtual TResult Visit( ISyntaxNode node ) {
            return node != null
                ? ( node ).Accept( this )
                : default( TResult );
        }

        private TResult VisitDefault( ISyntaxNode node ) {
            return default( TResult );
        }

        public virtual TResult VisitCaseCondition( CaseCondition node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitFunctionExpression( FunctionExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitVariableFunctionCallExpression( VariableFunctionCallExpression node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitAssignStatement( AssignStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitExitloopStatement( ExitloopStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitSelectCase( SelectCase node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitSwitchCase( SwitchCase node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitVariableFunctionCallStatement( VariableFunctionCallStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitStaticDeclarationStatement( StaticDeclarationStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitBlockStatement( BlockStatement node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitToken( TokenNode node ) {
            return VisitDefault( node );
        }

        public virtual TResult VisitAutoitParameter( AutoitParameter node ) {
            return VisitDefault( node );
        }
    }
}
