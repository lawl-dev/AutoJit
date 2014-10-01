using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public sealed class CSharpSkeletonFactory : ICSharpSkeletonFactory
    {
        public NamespaceDeclarationSyntax EmbedInClassTemplate(
            List<MemberDeclarationSyntax> memberDeclarationSyntaxs,
            string runtimeFieldName,
            string className,
            string contextInstanceName ) {
            var constructorDeclarationSyntax = SyntaxFactory.ConstructorDeclaration(
                SyntaxFactory.Identifier(
                    className ) )
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(
                            SyntaxKind.PublicKeyword ) ) )
                .WithBody(
                    SyntaxFactory.Block(
                        SyntaxFactory.List<StatementSyntax>(
                            new StatementSyntax[] {
                                SyntaxFactory.ExpressionStatement(
                                    SyntaxFactory.BinaryExpression(
                                        SyntaxKind.SimpleAssignmentExpression,
                                        SyntaxFactory.IdentifierName(
                                            contextInstanceName ),
                                        SyntaxFactory.ObjectCreationExpression(
                                            SyntaxFactory.GenericName(
                                                SyntaxFactory.Identifier(
                                                    @"AutoitContext" ) )
                                                .WithTypeArgumentList(
                                                    SyntaxFactory.TypeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                            SyntaxFactory.IdentifierName(
                                                                className ) ) ) ) )
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                        SyntaxFactory.Argument(
                                                            SyntaxFactory.ThisExpression() ) ) ) ) ) ),
                                SyntaxFactory.ExpressionStatement(
                                    SyntaxFactory.BinaryExpression(
                                        SyntaxKind.SimpleAssignmentExpression,
                                        SyntaxFactory.IdentifierName(
                                            runtimeFieldName ),
                                        SyntaxFactory.ObjectCreationExpression(
                                            SyntaxFactory.GenericName(
                                                SyntaxFactory.Identifier(
                                                    @"AutoitRuntime" ) )
                                                .WithTypeArgumentList(
                                                    SyntaxFactory.TypeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                            SyntaxFactory.IdentifierName(
                                                                className ) ) ) ) )
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                        SyntaxFactory.Argument(
                                                            SyntaxFactory.IdentifierName(
                                                                contextInstanceName ) ) ) ) ) ) ),
                                SyntaxFactory.ExpressionStatement(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.IdentifierName(
                                            @"Main" ) ) )
                            } ) ) );

            memberDeclarationSyntaxs.Add( constructorDeclarationSyntax );

            var declarationSyntaxs = SyntaxFactory.List<MemberDeclarationSyntax>(
                new MemberDeclarationSyntax[] {
                    SyntaxFactory.FieldDeclaration(
                        SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.GenericName(
                                SyntaxFactory.Identifier(
                                    @"AutoitContext" ) )
                                .WithTypeArgumentList(
                                    SyntaxFactory.TypeArgumentList(
                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                            SyntaxFactory.IdentifierName(
                                                className ) ) ) ) )
                            .WithVariables(
                                SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    SyntaxFactory.VariableDeclarator(
                                        SyntaxFactory.Identifier(
                                            contextInstanceName ) ) ) ) )
                        .WithModifiers(
                            SyntaxFactory.TokenList(
                                SyntaxFactory.Token(
                                    SyntaxKind.PrivateKeyword ) ) ),
                    SyntaxFactory.FieldDeclaration(
                        SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.GenericName(
                                SyntaxFactory.Identifier(
                                    @"AutoitRuntime" ) )
                                .WithTypeArgumentList(
                                    SyntaxFactory.TypeArgumentList(
                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                            SyntaxFactory.IdentifierName(
                                                className ) ) ) ) )
                            .WithVariables(
                                SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    SyntaxFactory.VariableDeclarator(
                                        SyntaxFactory.Identifier(
                                            runtimeFieldName ) ) ) ) )
                        .WithModifiers(
                            SyntaxFactory.TokenList(
                                SyntaxFactory.Token(
                                    SyntaxKind.PrivateKeyword ) ) )
                } );

            declarationSyntaxs = declarationSyntaxs.AddRange( memberDeclarationSyntaxs );

            return SyntaxFactory.NamespaceDeclaration(
                SyntaxFactory.IdentifierName(
                    "AutoJITScript" ) )
                .WithMembers(
                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                        SyntaxFactory.ClassDeclaration(
                            className )
                            .WithModifiers(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.Token(
                                        SyntaxKind.PublicKeyword ) ) )
                            .WithMembers(
                                declarationSyntaxs ) ).Add( GetEntryPointClass( className ) ) );
        }

        private MemberDeclarationSyntax GetEntryPointClass( string className ) {
            return SyntaxFactory.ClassDeclaration(
                @"Program" )
                .WithMembers(
                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                        SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.PredefinedType(
                                SyntaxFactory.Token(
                                    SyntaxKind.VoidKeyword ) ),
                            SyntaxFactory.Identifier(
                                @"Main" ) )
                            .WithModifiers(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.Token(
                                        SyntaxKind.StaticKeyword ) ) )
                            .WithParameterList(
                                SyntaxFactory.ParameterList(
                                    SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                                        SyntaxFactory.Parameter(
                                            SyntaxFactory.Identifier(
                                                @"args" ) )
                                            .WithType(
                                                SyntaxFactory.ArrayType(
                                                    SyntaxFactory.PredefinedType(
                                                        SyntaxFactory.Token(
                                                            SyntaxKind.StringKeyword ) ) )
                                                    .WithRankSpecifiers(
                                                        SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>(
                                                            SyntaxFactory.ArrayRankSpecifier(
                                                                SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                                                                    SyntaxFactory.OmittedArraySizeExpression() ) ) ) ) ) ) ) )
                            .WithBody(
                                SyntaxFactory.Block(
                                    SyntaxFactory.SingletonList<StatementSyntax>(
                                        SyntaxFactory.ExpressionStatement(
                                            SyntaxFactory.ObjectCreationExpression(
                                                SyntaxFactory.IdentifierName(
                                                    className ) )
                                                .WithArgumentList(
                                                    SyntaxFactory.ArgumentList() ) ) ) ) ) ) );
        }
    }
}
