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
    internal sealed class AutoitGlobalDeclarationStatementConverter : AutoitStatementConverterBase<GlobalDeclarationStatement>
    {
        public AutoitGlobalDeclarationStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(GlobalDeclarationStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            context.PushGlobalVariable( statement.VariableExpression.IdentifierName, DeclareGlobal( statement ) );
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

        private StatementSyntax AssignArray( GlobalDeclarationStatement statement, IContextService context ) {
            return CSharpStatementFactory.CreateInvocationExpression(
                statement.VariableExpression.IdentifierName, CompilerHelper.GetVariantMemberName( x => x.InitArray( null ) ),
                new CSharpParameterInfo( Convert(statement.InitExpression, context), false )
                    .ToEnumerable() ).ToStatementSyntax();
        }

        private FieldDeclarationSyntax DeclareGlobal( GlobalDeclarationStatement node ) {
            var variableDeclarationSyntax = DeclareVariable( node );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( GlobalDeclarationStatement node ) {
            var declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, node.VariableExpression.IdentifierName );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( GlobalDeclarationStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, Convert(node.VariableExpression, context),
                Convert(node.InitExpression, context) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( GlobalDeclarationStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( node.VariableExpression.IdentifierName ),
                GetArrayInitExpression( node, context ) )
                .ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( GlobalDeclarationStatement node, IContextService context ) {
            var openBracketToken =
                ( (ArrayExpression) node.VariableExpression ).AccessParameter.Select(
                    x => Convert(x, context) ).ToSeparatedSyntaxList();
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
