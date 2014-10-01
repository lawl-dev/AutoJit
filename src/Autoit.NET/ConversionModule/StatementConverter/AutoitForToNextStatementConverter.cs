using System;
using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Visitor;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitForToNextStatementConverter : AutoitStatementConverterBase<ForToNextStatement>
    {
        public AutoitForToNextStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService) {}

        public override IEnumerable<StatementSyntax> Convert(ForToNextStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            context.RegisterLoop();

            var exitLoopLabelName = context.GetExitLoopLabelName();
            var coninueLoopLabelName = context.GetConinueLoopLabelName();

            var handlerName = GetHandlerName();
            var handlerObjectExpression = CreateHandlerObject( statement, context );
            var indexHandlerExpression = GetIndexFromHandlerExpression( handlerName );
            var indexVariableDeclaration = GetIndexVariableDeclaration( statement );
            var condition = GetConditionExpression( handlerName );
            var getNextIndexEpression = GetNextIndexExpression( statement, handlerName );
            var forStatementSyntax = CreateForStatement( statement, context, coninueLoopLabelName );

            toReturn.Add( DeclareLoopObject( handlerName, handlerObjectExpression ) );
            toReturn.Add( InitLoopObject( handlerName, handlerObjectExpression ) );
            toReturn.Add( getNextIndexEpression.ToStatementSyntax() );

            if ( !context.IsDeclared( statement.VariableExpression.IdentifierName ) ) {
                context.Declare( statement.VariableExpression.IdentifierName );
                var localDeclarationStatementSyntax = GetIndexLocalDeclaration( statement, indexVariableDeclaration );
                toReturn.Add( localDeclarationStatementSyntax );
            }
            else {
                forStatementSyntax.WithInitializers( indexHandlerExpression );
            }

            forStatementSyntax = forStatementSyntax.WithCondition( condition )
                .WithIncrementors( getNextIndexEpression.ToSeparatedSyntaxList<ExpressionSyntax>() );

            toReturn.Add( forStatementSyntax );

            toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );
            context.UnregisterLoop();

            return toReturn;
        }

        private static LocalDeclarationStatementSyntax GetIndexLocalDeclaration(
            ForToNextStatement node,
            VariableDeclarationSyntax indexVariableDeclaration ) {
            return SyntaxFactory.LocalDeclarationStatement(
                indexVariableDeclaration.WithVariables(
                    SyntaxFactory.VariableDeclarator( node.VariableExpression.IdentifierName )
                        .WithInitializer( SyntaxFactory.EqualsValueClause( SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ) )
                        .ToSeparatedSyntaxList() ) );
        }

        private ForStatementSyntax CreateForStatement( ForToNextStatement node, IContextService context, string continueCaseLabelName ) {
            var block = node.Block.SelectMany( x => ConvertGeneric(x, context) ).ToList();
            block.Add( SyntaxFactory.LabeledStatement( continueCaseLabelName, SyntaxFactory.EmptyStatement() ) );
            return
                SyntaxFactory.ForStatement( block.ToBlock() );
        }

        private static BinaryExpressionSyntax GetNextIndexExpression( ForToNextStatement node, string handlerName ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.IdentifierName(
                    node.VariableExpression.IdentifierName ),
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(
                        handlerName ),
                    SyntaxFactory.IdentifierName(
                        @"Index" ) ) );
        }

        private static InvocationExpressionSyntax GetConditionExpression( string handlerName ) {
            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( handlerName ), SyntaxFactory.IdentifierName( "MoveNext" ) )
                );
        }

        private static VariableDeclarationSyntax GetIndexVariableDeclaration( ForToNextStatement node ) {
            return SyntaxFactory.VariableDeclaration(
                SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                SyntaxFactory.VariableDeclarator( node.VariableExpression.IdentifierName ).ToSeparatedSyntaxList() );
        }

        private static SeparatedSyntaxList<ExpressionSyntax> GetIndexFromHandlerExpression( string handlerName ) {
            return new SeparatedSyntaxList<ExpressionSyntax>().Add(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( handlerName ), SyntaxFactory.IdentifierName( "Index" ) ) );
        }

        private static string GetHandlerName() {
            return string.Format( "loopHandler{0}", Guid.NewGuid().ToString( "N" ) );
        }

        private StatementSyntax InitLoopObject( string looperName, ObjectCreationExpressionSyntax forLooperObj ) {
            return
                SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( looperName ), forLooperObj )
                    .ToStatementSyntax();
        }

        private LocalDeclarationStatementSyntax DeclareLoopObject( string looperName, ObjectCreationExpressionSyntax forLooperObj ) {
            return SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.IdentifierName( typeof (ForToNextLooper).Name ), SyntaxFactory.VariableDeclarator( looperName ).ToSeparatedSyntaxList() ) );
        }

        private ObjectCreationExpressionSyntax CreateHandlerObject( ForToNextStatement node, IContextService context ) {
            return SyntaxFactory.ObjectCreationExpression( SyntaxFactory.IdentifierName( typeof (ForToNextLooper).Name ) )
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList<ArgumentSyntax>(
                            new[] {
                                SyntaxFactory.Argument( Convert(node.StartExpression, context) ),
                                SyntaxFactory.Argument( Convert(node.EndExpression, context) ),
                                SyntaxFactory.Argument(
                                    node.StepExpression != null
                                        ? Convert(node.StepExpression, context)
                                        : SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NullLiteralExpression ) )
                            } ) ) );
        }
    }
}
