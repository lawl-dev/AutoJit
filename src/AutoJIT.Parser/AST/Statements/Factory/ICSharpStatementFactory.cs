using System.Collections.Generic;
using AutoJIT.Parser.Helper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.AST.Statements.Factory
{
    public interface ICSharpStatementFactory
    {
        InvocationExpressionSyntax CreateInvocationExpression( string functionName, IEnumerable<CSharpParameterInfo> parameter );
        InvocationExpressionSyntax CreateInvocationExpression( string runtimeInstanceName, string functionName, IEnumerable<CSharpParameterInfo> parameter );
        VariableDeclarationSyntax CreateVariable( string typeName, string variableName, ExpressionSyntax expressionToAssign );
        IfStatementSyntax CreateIfStatement( ExpressionSyntax condition, IEnumerable<StatementSyntax> statementNodes );
        ReturnStatementSyntax CreateReturn( ExpressionSyntax toReturn );
        WhileStatementSyntax CreateWhileStatement( ExpressionSyntax whileCondition, IEnumerable<StatementSyntax> whileBlock );
        FieldDeclarationSyntax CreateFieldDeclarationStatement( VariableDeclarationSyntax variableDeclarationSyntax );
        LocalDeclarationStatementSyntax CreateLocalDeclarationStatement( VariableDeclarationSyntax variableDeclarationSyntax );
        VariableDeclarationSyntax CreateVariable( string typeName, string identifierName );
        MemberAccessExpressionSyntax CreateMemberAccessExpression( string contextInstanceName, string macroName );
        StatementSyntax CreateForInStatement( string identifierName, ExpressionSyntax expression, IEnumerable<StatementSyntax> statements );
        VariableDeclarationSyntax CreateVariable( TypeSyntax getGenericName, string forConditionName, ExpressionSyntax expressionToAssign );
        VariableDeclarationSyntax CreateVariableUninit( string typeName, string identifierName );
    }
}
