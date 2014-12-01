using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Visitor
{
    public abstract class SyntaxRewriterBase : SyntaxVisitorBase<ISyntaxNode>
    {
        public override ISyntaxNode VisitArrayExpression( ArrayExpression node ) {
            IEnumerable<IExpressionNode> accessParameter = node.AccessParameter.Select( x => (IExpressionNode) Visit( x ) );
            var identifierName = (TokenNode)Visit(node.IdentifierName);
            return node.Update( identifierName, accessParameter.ToList() );
        }

        public override ISyntaxNode VisitArrayInitExpression( ArrayInitExpression node ) {
            List<IExpressionNode> toAssign = node.ToAssign.Select( x => (IExpressionNode) Visit( x ) ).ToList();
            return node.Update( toAssign );
        }

        public override ISyntaxNode VisitAssignStatement( AssignStatement node ) {
            var variable = (VariableExpression)Visit( node.Variable );
            var expressionToAssign = (IExpressionNode) Visit( node.ExpressionToAssign );
            TokenNode @operator = (TokenNode)Visit(node.Operator);
            return node.Update( variable, expressionToAssign, @operator );
        }

        public override ISyntaxNode VisitAutoitScriptRoot( AutoitScriptRoot node ) {
            var mainFunction = (BlockStatement) Visit( node.MainFunction );
            List<Function> functions = node.Functions.Select( x => (Function) Visit( x ) ).ToList();
            PragmaOptions pragmaOptions = node.PragmaOptions;
            return node.Update( mainFunction, functions, pragmaOptions );
        }

        public override ISyntaxNode VisitBinaryExpression( BinaryExpression node ) {
            var left = (IExpressionNode) Visit( node.Left );
            var right = (IExpressionNode) Visit( node.Right );
            TokenNode @operator = (TokenNode) Visit(node.Operator);
            return node.Update( left, right, @operator );
        }

        public override ISyntaxNode VisitBooleanNegateExpression( BooleanNegateExpression node ) {
            var left = (IExpressionNode) Visit( node.Left );
            TokenNode @operator = (TokenNode)Visit(node.Operator);
            return node.Update( left, @operator );
        }

        public override ISyntaxNode VisitCallExpression( CallExpression node ) {
            List<IExpressionNode> parameter = node.Parameter.Select( x => (IExpressionNode) Visit( x ) ).ToList();
            var identifierName = (TokenNode)Visit(node.IdentifierName);
            return node.Update( parameter, identifierName );
        }

        public override ISyntaxNode VisitCaseCondition( CaseCondition node ) {
            var left = (IExpressionNode) Visit( node.Left );
            var right = (IExpressionNode) Visit( node.Right );
            return node.Update( left, right );
        }

        public override ISyntaxNode VisitContinueCaseStatement( ContinueCaseStatement node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitContinueLoopStatement( ContinueLoopStatement node ) {
            TokenNode level = (TokenNode) Visit(node.Level);
            return node.Update( level );
        }

        public override ISyntaxNode VisitDefaultExpression( DefaultExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitDimStatement( DimStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var initExpression = (IExpressionNode) Visit( node.InitExpression );
            return node.Update( variableExpression, initExpression );
        }

        public override ISyntaxNode VisitDoUntilStatement( DoUntilStatement node ) {
            var condition = (IExpressionNode) Visit( node.Condition );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( condition, block );
        }

        public override ISyntaxNode VisitLocalEnumDeclarationStatement( LocalEnumDeclarationStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var userInitExpression = (IExpressionNode) Visit( node.UserInitExpression );
            var autoInitExpression = (IExpressionNode) Visit( node.AutoInitExpression );
            return node.Update( variableExpression, userInitExpression, autoInitExpression );
        }

        public override ISyntaxNode VisitGlobalEnumDeclarationStatement( GlobalEnumDeclarationStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var userInitExpression = (IExpressionNode) Visit( node.UserInitExpression );
            var autoInitExpression = (IExpressionNode) Visit( node.AutoInitExpression );
            return node.Update( variableExpression, userInitExpression, autoInitExpression );
        }

        public override ISyntaxNode VisitExitStatement( ExitStatement node ) {
            var exitExpression = (IExpressionNode) Visit( node.ExitExpression );
            return node.Update( exitExpression );
        }

        public override ISyntaxNode VisitExitloopStatement( ExitloopStatement node ) {
            TokenNode level = (TokenNode) Visit(node.Level);
            return node.Update( level );
        }

        public override ISyntaxNode VisitFalseLiteralExpression( FalseLiteralExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitForInStatement( ForInStatement node ) {
            var toEnumerate = (IExpressionNode) Visit( node.ToEnumerate );
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( toEnumerate, variableExpression, block );
        }

        public override ISyntaxNode VisitForToNextStatement( ForToNextStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var startExpression = (IExpressionNode) Visit( node.StartExpression );
            var endExpression = (IExpressionNode) Visit( node.EndExpression );
            var stepExpression = (IExpressionNode) Visit( node.StepExpression );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( variableExpression, startExpression, endExpression, stepExpression, block );
        }

        public override ISyntaxNode VisitFunction( Function node ) {
            var name = (TokenNode)Visit( node.Name );
            var parameter = node.Parameter.Select( x => (AutoitParameter) Visit( x ) ).ToList();
            var statements = (BlockStatement)Visit(node.Statements);
            return node.Update( name, parameter, statements );
        }

        public override ISyntaxNode VisitFunctionCallStatement( FunctionCallStatement node ) {
            var functionCallExpression = (CallExpression) Visit( node.FunctionCallExpression );
            return node.Update( functionCallExpression );
        }

        public override ISyntaxNode VisitFunctionExpression( FunctionExpression node ) {
            var identifierName = (TokenNode)Visit(node.IdentifierName);
            return node.Update( identifierName );
        }

        public override ISyntaxNode VisitGlobalDeclarationStatement( GlobalDeclarationStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var initExpression = node.InitExpression != null ? (IExpressionNode) Visit( node.InitExpression ) : null;
            bool isConst = node.IsConst;
            return node.Update( variableExpression, initExpression, isConst );
        }

        public override ISyntaxNode VisitIfElseStatement( IfElseStatement node ) {
            var condition = (IExpressionNode) Visit( node.Condition );
            var ifBlock = (BlockStatement) Visit( node.IfBlock );
            List<IExpressionNode> elseIfConditions = node.ElseIfConditions.Select( x => (IExpressionNode) Visit( x ) ).ToList();
            List<BlockStatement> elseIfBlocks = node.ElseIfBlocks.Select( x => (BlockStatement) Visit( x ) ).ToList();
            var elseBlock = (BlockStatement) Visit( node.ElseBlock );
            return node.Update( condition, ifBlock, elseIfConditions, elseIfBlocks, elseBlock );
        }

        public override ISyntaxNode VisitInitDefaultParameterStatement( InitDefaultParameterStatement node ) {
            string parameterName = node.ParameterName;
            var defaultValue = (IExpressionNode) Visit( node.DefaultValue );
            return node.Update( parameterName, defaultValue );
        }

        public override ISyntaxNode VisitLocalDeclarationStatement( LocalDeclarationStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var initExpression = (IExpressionNode) Visit( node.InitExpression );
            bool isConst = node.IsConst;
            return node.Update( variableExpression, initExpression, isConst );
        }

        public override ISyntaxNode VisitMacroExpression( MacroExpression node ) {
            var macroName = (TokenNode)Visit(node.MacroName);
            return node.Update( macroName );
        }

        public override ISyntaxNode VisitNegateExpression( NegateExpression node ) {
            var expressionNode = (IExpressionNode) Visit( node.ExpressionNode );
            return node.Update( expressionNode );
        }

        public override ISyntaxNode VisitNullExpression( NullExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitNumericLiteralExpression( NumericLiteralExpression node ) {
            var signOperators = node.SignOperators.Select( x=>(TokenNode)Visit( x ) ).ToList();
            TokenNode literalToken = (TokenNode)Visit(node.LiteralToken);
            return node.Update( literalToken, signOperators );
        }

        public override ISyntaxNode VisitReDimStatement( ReDimStatement node ) {
            var arrayExpression = (ArrayExpression) Visit( node.ArrayExpression );
            return node.Update( arrayExpression );
        }

        public override ISyntaxNode VisitReturnStatement( ReturnStatement node ) {
            var returnExpression = (IExpressionNode) Visit( node.ReturnExpression );
            return node.Update( returnExpression );
        }

        public override ISyntaxNode VisitSelectCase( SelectCase node ) {
            var condition = (IExpressionNode) Visit( node.Condition );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( condition, block );
        }

        public override ISyntaxNode VisitSelectCaseStatement( SelectCaseStatement node ) {
            IEnumerable<SelectCase> selectCases = node.Cases.Select( x => (SelectCase) Visit( x ) );
            var @else = (BlockStatement) Visit( node.Else );
            return node.Update( selectCases, @else );
        }

        public override ISyntaxNode VisitStaticDeclarationStatement( StaticDeclarationStatement node ) {
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            var initExpression = (IExpressionNode) Visit( node.InitExpression );
            bool isConst = node.IsConst;
            return node.Update( variableExpression, initExpression, isConst );
        }

        public override ISyntaxNode VisitStringLiteralExpression( StringLiteralExpression node ) {
            TokenNode literalToken = (TokenNode) Visit(node.LiteralToken);
            return node.Update( literalToken );
        }

        public override ISyntaxNode VisitSwitchCase( SwitchCase node ) {
            IEnumerable<CaseCondition> conditions = node.Conditions.Select( x => (CaseCondition) Visit( x ) );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( conditions, block );
        }

        public override ISyntaxNode VisitSwitchCaseStatement( SwitchCaseStatement node ) {
            IEnumerable<SwitchCase> cases = node.Cases.Select( x => (SwitchCase) Visit( x ) );
            var condition = (IExpressionNode) Visit( node.Condition );
            var @else = (BlockStatement) Visit( node.Else );
            return node.Update( condition, cases, @else );
        }

        public override ISyntaxNode VisitTernaryExpression( TernaryExpression node ) {
            var condition = (IExpressionNode) Visit( node.Condition );
            var ifTrue = (IExpressionNode) Visit( node.IfTrue );
            var ifFalse = (IExpressionNode) Visit( node.IfFalse );
            return node.Update( condition, ifTrue, ifFalse );
        }

        public override ISyntaxNode VisitTrueLiteralExpression( TrueLiteralExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitUserfunctionCallExpression( UserfunctionCallExpression node ) {
            var identifierName = (TokenNode)Visit(node.IdentifierName);
            IEnumerable<IExpressionNode> parameter = node.Parameter.Select( x => (IExpressionNode) Visit( x ) );
            return node.Update( parameter, identifierName );
        }

        public override ISyntaxNode VisitVariableExpression( VariableExpression node ) {
            var identifierName = (TokenNode)Visit(node.IdentifierName);
            return node.Update( identifierName );
        }

        public override ISyntaxNode VisitVariableFunctionCallExpression( VariableFunctionCallExpression node ) {
            IEnumerable<IExpressionNode> parameter = node.Parameter.Select( x => (IExpressionNode) Visit( x ) );
            var variableExpression = (VariableExpression) Visit( node.VariableExpression );
            return node.Update( variableExpression, parameter );
        }

        public override ISyntaxNode VisitVariableFunctionCallStatement( VariableFunctionCallStatement node ) {
            var variableFunctionCallExpression = (VariableFunctionCallExpression) Visit( node.VariableFunctionCallExpression );
            return node.Update( variableFunctionCallExpression );
        }

        public override ISyntaxNode VisitWhileStatement( WhileStatement node ) {
            var condition = (IExpressionNode) Visit( node.Condition );
            var block = (BlockStatement) Visit( node.Block );
            return node.Update( condition, block );
        }

        public override ISyntaxNode VisitBlockStatement( BlockStatement node ) {
            List<IStatementNode> block = node.Block.Select( x => (IStatementNode) Visit( x ) ).ToList();
            return node.Update( block );
        }

        public override ISyntaxNode VisitAutoitParameter( AutoitParameter node ) {
            var defaultValue = (IExpressionNode)Visit(node.DefaultValue);
            var parameterName = (TokenNode)Visit( node.ParameterName );
            return node.Update(parameterName, defaultValue, node.IsByRef, node.IsConst);
        }

        public override ISyntaxNode VisitProperty( PropertyDeclarationStatement node ) {
            var getter = (PropertyGetter)Visit(node.PropertyGetter);
            var setter = (PropertySetter)Visit( node.PropertySetter );
            var variable = (VariableExpression)Visit(node.VariableExpression);
            return node.Update( variable, getter, setter );
        }

        public override ISyntaxNode VisitPropertyGetter( PropertyGetter node ) {
            var block = (BlockStatement)Visit( node.StatementBlock );
            return node.Update( block );
        }

        public override ISyntaxNode VisitPropertySetter( PropertySetter node ) {
            var block = (BlockStatement)Visit(node.StatementBlock);
            return node.Update(block);
        }

        public override ISyntaxNode VisitEmptyStatement( EmptyStatement node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitValueExpression( ValueExpression node ) {
            return node.Update();
        }

        public override ISyntaxNode VisitToken( TokenNode node ) {
            return node.Update();
        }
    }
}
