using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Factory
{
	public interface IAutoitStatementFactory
	{
		AssignStatement CreateAssignStatement( VariableExpression variableExpression, IExpressionNode expression, Token @operator );
		ContinueCaseStatement CreateContinueCaseStatement();
		ContinueLoopStatement CreateContinueloopStatement( int level );
		DimStatement CreateDimStatement( VariableExpression variableExpression, IExpressionNode initExpression );
		DoUntilStatement CreateDoUntilStatement( IExpressionNode condition, List<IStatementNode> block );
		ExitloopStatement CreateExitloopStatement( int level );
		ExitStatement CreateExitStatement( IExpressionNode exitCode );
		ForInStatement CreateForInStatement( VariableExpression variableName, IExpressionNode toEnumerate, List<IStatementNode> block );

		ForToNextStatement CreateForToNextStatement( VariableExpression variableExpression, IExpressionNode startExpression, IExpressionNode endExpression, IExpressionNode stepExpression, List<IStatementNode> block );

		FunctionCallStatement CreateFunctionCallStatement( CallExpression functionCallExpression );
		GlobalDeclarationStatement CreateGlobalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst );

		EnumDeclarationStatement CreateEnumDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, IExpressionNode autoInitExpression, bool global );

		IfElseStatement CreateIfElseStatement( IExpressionNode condition, List<IStatementNode> ifBlock, Queue<IExpressionNode> elseIfConditions, Queue<List<IStatementNode>> elseIfBlocks, IEnumerable<IStatementNode> elseBlock );

		LocalDeclarationStatement CreateLocalDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression, bool isConst );

		ReDimStatement CreateReDimStatement( ArrayExpression arrayExpression );
		ReturnStatement CreateReturnStatement( IExpressionNode returnExpression );
		SelectCaseStatement CreateSelectStatement( IEnumerable<SelectCase> cases, IEnumerable<IStatementNode> elseStatements );
		WhileStatement CreateWhileStatement( IExpressionNode condition, List<IStatementNode> block );
		IStatementNode CreateStaticDeclarationStatement( VariableExpression variableExpression, IExpressionNode initExpression );
	}
}
