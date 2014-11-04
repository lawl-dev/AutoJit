using System;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Visitor
{
    public class SyntaxRewriterBase : SyntaxVisitorBase<ISyntaxNode>
    {
        public override ISyntaxNode VisitArrayExpression( ArrayExpression node ) {
            var accessParameter = node.AccessParameter.Select( x => (IExpressionNode) Visit( x ) );
            return node.Update( node.IdentifierName, accessParameter );
        }

        public override ISyntaxNode VisitArrayInitExpression( ArrayInitExpression node ) {
            var toAssign = node.ToAssign.Select( x=>(IExpressionNode)Visit( x ) );
            return node.Update( toAssign );
        }
        
        public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
            var variable = (VariableExpression)Visit(node.Variable);
            var expressionToAssign = (IExpressionNode)Visit(node.ExpressionToAssign);
            var @operator = node.Operator;
            return node.Update(variable, expressionToAssign, @operator);
        }

        public override ISyntaxNode VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            var mainFunction = (Function)Visit( node.MainFunction );
            var functions = node.Functions.Select( x=>(Function)Visit(x) );
            var pragmaOptions = node.PragmaOptions;
            return node.Update( mainFunction, functions, pragmaOptions );
        }

        public override ISyntaxNode VisitBinaryExpression( BinaryExpression node ) {
            var left = (IExpressionNode)Visit( node.Left );
            var right = (IExpressionNode)Visit( node.Right );
            var @operator = node.Operator;
            return node.Update( left, right, @operator );
        }

        public override ISyntaxNode VisitBooleanNegateExpression( BooleanNegateExpression node ) {
            var left = (IExpressionNode)Visit( node.Left );
            var @operator = node.Operator;
            return node.Update( left, @operator );
        }

        public override ISyntaxNode VisitCallExpression( CallExpression node ) {
            var parameter = node.Parameter.Select( x=>(IExpressionNode)Visit( x ) );
            var identifierName = node.IdentifierName;
            return node.Update( parameter, identifierName );
        }

        public override ISyntaxNode VisitCaseCondition( CaseCondition node ) {
            var left = (IExpressionNode)Visit( node.Left );
            var right = (IExpressionNode)Visit( node.Right );
            return node.Update( left, right );
        }

        public override ISyntaxNode VisitContinueCaseStatement( ContinueCaseStatement node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitContinueLoopStatement( ContinueLoopStatement node ) {
            var level = node.Level;
            return node.Update( level );
        }

        public override ISyntaxNode VisitDefaultExpression( DefaultExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitDimStatement( DimStatement node ) {
            var variableExpression = (VariableExpression)Visit( node.VariableExpression );
            var initExpression = (IExpressionNode)Visit( node.InitExpression );
            return node.Update( variableExpression, initExpression );
        }

        public override ISyntaxNode VisitDoUntilStatement( DoUntilStatement node ) {
            var condition = (IExpressionNode)Visit( node.Condition );
            var block = node.Block.Select( x=>(IStatementNode)x );
            return node.Update( condition, block );
        }

        public override ISyntaxNode VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {
            var variableExpression = (VariableExpression)Visit(node.VariableExpression);
            var userInitExpression = (IExpressionNode)Visit(node.UserInitExpression);
            var autoInitExpression = (IExpressionNode)Visit(node.AutoInitExpression);
            return node.Update( variableExpression, userInitExpression, autoInitExpression );
        }

        public override ISyntaxNode VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node ) {
            var variableExpression = (VariableExpression)Visit(node.VariableExpression);
            var userInitExpression = (IExpressionNode)Visit(node.UserInitExpression);
            var autoInitExpression = (IExpressionNode)Visit(node.AutoInitExpression);
            return node.Update( variableExpression, userInitExpression, autoInitExpression );
        }

        public override ISyntaxNode VisitExitStatement( ExitStatement node ) {
            var exitExpression = (IExpressionNode)Visit( node.ExitExpression );
            return node.Update( exitExpression );
        }

        public override ISyntaxNode VisitExitloopStatement( ExitloopStatement node ) {
            var level = node.Level;
            return node.Update( level );
        }

        public override ISyntaxNode VisitFalseLiteralExpression( FalseLiteralExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitForInStatement( ForInStatement node ) {
            var toEnumerate = (IExpressionNode)Visit( node.ToEnumerate );
            var variableExpression = (VariableExpression)Visit( node.VariableExpression );
            var block = node.Block.Select( x=>(IStatementNode)Visit(x) );
            return node.Update( toEnumerate, variableExpression, block );
        }

        public override ISyntaxNode VisitForToNextStatement( ForToNextStatement node ) {
            var variableExpression = (VariableExpression)Visit( node.VariableExpression );
            var startExpression = (IExpressionNode)Visit( node.StartExpression );
            var endExpression = (IExpressionNode)Visit( node.EndExpression );
            var stepExpression = (IExpressionNode)Visit(node.StepExpression);
            var block = node.Block.Select( x=>(IStatementNode)Visit( x ) );
            return node.Update( variableExpression, startExpression, endExpression, stepExpression, block );
        }
    }
}