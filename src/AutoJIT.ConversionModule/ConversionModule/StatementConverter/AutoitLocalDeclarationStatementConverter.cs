using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitLocalDeclarationStatementConverter : AutoitStatementConverterBase<LocalDeclarationStatement>
    {
        public AutoitLocalDeclarationStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert(LocalDeclarationStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            if ( !context.IsDeclared( statement.VariableExpression.IdentifierName ) ) {
                context.Declare( statement.VariableExpression.IdentifierName );
                toReturn.Add( DeclareLocal( statement ) );
            }
            if ( statement.VariableExpression is ArrayExpression ) {
                toReturn.Add( InitArray( statement, context ) );
                if ( statement.InitExpression != null ) {
                    toReturn.Add( AssignArray( statement, context ) );
                }
                return toReturn;
            }
            if ( statement.InitExpression != null ) {
                toReturn.Add( AssignVariable( statement, context ) );
            }
            return toReturn;
        }

        private StatementSyntax AssignArray( LocalDeclarationStatement statement, IContextService contextService ) {
            return CSharpStatementFactory.CreateInvocationExpression(
                statement.VariableExpression.IdentifierName, CompilerHelper.GetVariantMemberName( x => x.InitArray( null ) ),
                new CSharpParameterInfo( Convert(statement.InitExpression, contextService), false )
                    .ToEnumerable() ).ToStatementSyntax();
        }

        private StatementSyntax DeclareLocal( LocalDeclarationStatement statement ) {
            var variableDeclarationSyntax = DeclareVariable( statement );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( LocalDeclarationStatement statement ) {
            var declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, statement.VariableExpression.IdentifierName );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( LocalDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( statement.VariableExpression.IdentifierName ),
                Convert(statement.InitExpression, contextService) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( LocalDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( statement.VariableExpression.IdentifierName ),
                GetArrayInitExpression( statement, contextService ) )
                .ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( LocalDeclarationStatement statement, IContextService contextService ) {
            var openBracketToken =
                ( (ArrayExpression) statement.VariableExpression ).AccessParameter.Select(
                    x => Convert(x, contextService) ).ToSeparatedSyntaxList();
            var arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression(
                SyntaxFactory.ArrayType(
                    SyntaxFactory.IdentifierName(
                        typeof (Variant).Name ) )
                    .WithRankSpecifiers(
                        SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(typeof(Variant).Name),
                    SyntaxFactory.IdentifierName(CompilerHelper.GetVariantMemberName(x => Variant.CreateArray(null)))))
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.Argument(arrayCreationExpressionSyntax).ToSeparatedSyntaxList()));
        }
    }
}
