using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitStaticDeclarationStatementConverter : AutoitStatementConverterBase<StaticDeclarationStatement>
    {
        public AutoitStaticDeclarationStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( StaticDeclarationStatement statement, IContextService context ) {
            if ( context.IsDeclaredStatic( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ) {
                throw new InvalidOperationException();
            }

            var toReturn = new List<StatementSyntax>();

            context.RegisterStatic( statement.VariableExpression.IdentifierName.Token.Value.StringValue );
            context.PushGlobalVariable( statement.VariableExpression.IdentifierName.Token.Value.StringValue, DeclareGlobal( statement, context ) );
            if ( statement.VariableExpression is ArrayExpression ) {
                var iniStatements = new List<StatementSyntax>();
                iniStatements.Add( InitArray( statement, context ) );
                if ( statement.InitExpression != null ) {
                    iniStatements.Add( AssignArray( statement, context ) );
                }

                toReturn.Add( AddNullCheck( statement, iniStatements, context ) );

                return toReturn;
            }
            toReturn.Add(
                statement.InitExpression != null
                    ? AddNullCheck( statement, AssignVariable( statement, context ).ToEnumerable(), context )
                    : AddNullCheck( statement, AssignNullVariant( statement, context ).ToEnumerable(), context ) );

            return toReturn;
        }

        private StatementSyntax AddNullCheck( StaticDeclarationStatement statement, IEnumerable<StatementSyntax> iniStatements, IContextService context ) {
            IfStatementSyntax ifStatement = SyntaxFactory.IfStatement( SyntaxFactory.BinaryExpression( SyntaxKind.EqualsExpression, SyntaxFactory.IdentifierName( context.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ), iniStatements.ToBlock() );
            return ifStatement;
        }

        private StatementSyntax AssignArray( StaticDeclarationStatement statement, IContextService contextService ) {
            return CSharpStatementFactory.CreateInvocationExpression( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ), CompilerHelper.GetVariantMemberName( x => x.InitArray( null ) ), new CSharpParameterInfo( ConvertGeneric( statement.InitExpression, contextService ), false ).ToEnumerable() ).ToStatementSyntax();
        }

        private FieldDeclarationSyntax DeclareGlobal( StaticDeclarationStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( StaticDeclarationStatement statement, IContextService context ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariableUninit( typeof (Variant).Name, context.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( StaticDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), ConvertGeneric( statement.InitExpression, contextService ) ).ToStatementSyntax();
        }

        private StatementSyntax AssignNullVariant( StaticDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( StaticDeclarationStatement statement, IContextService contextService ) {
            BinaryExpressionSyntax binaryExpression = SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), GetArrayInitExpression( statement, contextService ) );

            return binaryExpression.ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( StaticDeclarationStatement statement, IContextService contextService ) {
            SeparatedSyntaxList<ExpressionSyntax> openBracketToken = ( (ArrayExpression) statement.VariableExpression ).AccessParameter.Select( x => ConvertGeneric( x, contextService ) ).ToSeparatedSyntaxList();
            ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression( SyntaxFactory.ArrayType( SyntaxFactory.IdentifierName( typeof (Variant).Name ) ).WithRankSpecifiers( SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.CreateArray( null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( arrayCreationExpressionSyntax ).ToSeparatedSyntaxList() ) );
        }
    }
}
