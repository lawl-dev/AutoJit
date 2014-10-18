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
    internal sealed class AutoitDimStatementConverter : AutoitStatementConverterBase<DimStatement>
    {
        public AutoitDimStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( DimStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();
            if ( !context.IsDeclared( statement.VariableExpression.IdentifierName ) ) {
                context.Declare( statement.VariableExpression.IdentifierName );

                toReturn.Add( DeclareLocal( statement ) );
            }
            if ( statement.VariableExpression is ArrayExpression ) {
                toReturn.Add( InitArray( statement, context ) );
            }
            if ( statement.InitExpression != null ) {
                toReturn.Add( AssignVariable( statement, context ) );
            }
            return toReturn;
        }

        private StatementSyntax DeclareLocal( DimStatement node ) {
            var variableDeclarationSyntax = DeclareVariable( node );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( DimStatement node ) {
            var declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, node.VariableExpression.IdentifierName );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( DimStatement node, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, Convert( node.VariableExpression, contextService ),
                Convert( node.InitExpression, contextService ) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( DimStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( node.VariableExpression.IdentifierName ),
                GetArrayInitExpression( node, context ) )
                .ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( DimStatement node, IContextService context ) {
            var openBracketToken =
                ( (ArrayExpression) node.VariableExpression ).AccessParameter.Select(
                    x => Convert( x, context ) ).ToSeparatedSyntaxList();
            var arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression(
                SyntaxFactory.ArrayType(
                    SyntaxFactory.IdentifierName(
                        typeof (Variant).Name ) )
                    .WithRankSpecifiers(
                        SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                    SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.CreateArray( null ) ) ) ) )
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.Argument( arrayCreationExpressionSyntax ).ToSeparatedSyntaxList() ) );
        }
    }
}
