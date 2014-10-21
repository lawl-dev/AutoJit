using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.AST.Statements.Factory
{
    public sealed class CSharpStatementFactory : ICSharpStatementFactory
    {
        public StatementSyntax CreateForInStatement( string identifierName, ExpressionSyntax expression, IEnumerable<StatementSyntax> statements ) {
            TypeSyntax typeName = SyntaxFactory.ParseTypeName( typeof (Variant).Name );
            return SyntaxFactory.ForEachStatement(
                typeName, identifierName, expression,
                statements.ToBlock() );
        }

        public InvocationExpressionSyntax CreateInvocationExpression( string functionName, IEnumerable<CSharpParameterInfo> parameter ) {
            return SyntaxFactory.InvocationExpression( SyntaxFactory.IdentifierName( functionName ), CreateArguments( parameter ) );
        }

        public InvocationExpressionSyntax CreateInvocationExpression(
            string runtimeInstanceName,
            string functionName,
            IEnumerable<CSharpParameterInfo> parameter ) {
            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName( runtimeInstanceName ),
                    SyntaxFactory.IdentifierName( functionName ) ), CreateArguments( parameter ) );
        }

        public VariableDeclarationSyntax CreateVariable( string typeName, string variableName, ExpressionSyntax expressionToAssign ) {
            return SyntaxFactory.VariableDeclaration(
                SyntaxFactory.IdentifierName( typeName ),
                SyntaxFactory.VariableDeclarator( variableName )
                    .WithInitializer( SyntaxFactory.EqualsValueClause( expressionToAssign ?? CreateNullExpression() ) )
                    .ToSeparatedSyntaxList() );
        }

        public VariableDeclarationSyntax CreateVariable( TypeSyntax typeName, string variableName, ExpressionSyntax expressionToAssign ) {
            return SyntaxFactory.VariableDeclaration(
                typeName,
                SyntaxFactory.VariableDeclarator( variableName )
                    .WithInitializer( SyntaxFactory.EqualsValueClause( expressionToAssign ?? CreateNullExpression() ) )
                    .ToSeparatedSyntaxList() );
        }

        public IfStatementSyntax CreateIfStatement( ExpressionSyntax condition, IEnumerable<StatementSyntax> statementNodes ) {
            return SyntaxFactory.IfStatement( condition, statementNodes.ToBlock() );
        }

        public ReturnStatementSyntax CreateReturn( ExpressionSyntax toReturn ) {
            return SyntaxFactory.ReturnStatement( toReturn );
        }

        public WhileStatementSyntax CreateWhileStatement( ExpressionSyntax whileCondition, IEnumerable<StatementSyntax> whileBlock ) {
            return SyntaxFactory.WhileStatement( whileCondition, whileBlock.ToBlock() );
        }

        public FieldDeclarationSyntax CreateFieldDeclarationStatement( VariableDeclarationSyntax variableDeclarationSyntax ) {
            return SyntaxFactory.FieldDeclaration( variableDeclarationSyntax );
        }

        public LocalDeclarationStatementSyntax CreateLocalDeclarationStatement( VariableDeclarationSyntax variableDeclarationSyntax ) {
            return SyntaxFactory.LocalDeclarationStatement( variableDeclarationSyntax );
        }

        public VariableDeclarationSyntax CreateVariable( string typeName, string identifierName ) {
            return CreateVariable( typeName, identifierName, CreateNullExpression() );
        }

        public MemberAccessExpressionSyntax CreateMemberAccessExpression( string contextInstanceName, string macroName ) {
            return SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( contextInstanceName ), SyntaxFactory.IdentifierName( macroName ) );
        }

        private ExpressionSyntax CreateNullExpression() {
            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                    SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) )
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.Argument(
                            SyntaxFactory.CastExpression(
                                SyntaxFactory.PredefinedType(
                                    SyntaxFactory.Token(
                                        SyntaxKind.ObjectKeyword ) ),
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.NullLiteralExpression ) ) ).ToSeparatedSyntaxList() ) );
        }

        private ArgumentListSyntax CreateArguments( IEnumerable<CSharpParameterInfo> parameter ) {
            return
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList(
                        parameter.Select(
                            x => SyntaxFactory.Argument( x.Parameter ).WithRefOrOutKeyword(
                                x.IsByRef
                                    ? SyntaxFactory.Token( SyntaxKind.RefKeyword )
                                    : SyntaxFactory.Token( SyntaxKind.None ) ) ) ) );
        }
    }
}
