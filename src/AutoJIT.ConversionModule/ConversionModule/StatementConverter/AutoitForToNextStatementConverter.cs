using System;
using System.Collections.Generic;
using System.Linq;
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
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( ForToNextStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            context.RegisterLoop();

            string exitLoopLabelName = context.GetExitLoopLabelName();
            string coninueLoopLabelName = context.GetConinueLoopLabelName();

            string handlerName = GetHandlerName();

            bool loacl = false;
            if ( !context.IsDeclaredLocal( statement.VariableExpression.IdentifierName ) ) {
                context.DeclareLocal( statement.VariableExpression.IdentifierName );
                loacl = true;
            }

            ObjectCreationExpressionSyntax handlerObjectExpression = CreateHandlerObject( statement, context );
            SeparatedSyntaxList<ExpressionSyntax> indexHandlerExpression = GetIndexFromHandlerExpression( handlerName );
            InvocationExpressionSyntax condition = GetConditionExpression( handlerName );
            ForStatementSyntax forStatementSyntax = CreateForStatement( statement, context, coninueLoopLabelName );

            toReturn.Add( DeclareLoopObject( handlerName, handlerObjectExpression ) );
            toReturn.Add( InitLoopObject( handlerName, handlerObjectExpression ) );
            

            if(loacl){
                VariableDeclarationSyntax indexVariableDeclaration = GetIndexVariableDeclaration(statement, context);
            
                LocalDeclarationStatementSyntax localDeclarationStatementSyntax = GetIndexLocalDeclaration( statement, indexVariableDeclaration, context );
                toReturn.Add( localDeclarationStatementSyntax );
            }
            else {
                forStatementSyntax.WithInitializers( indexHandlerExpression );
            }
            BinaryExpressionSyntax getNextIndexEpression = GetNextIndexExpression(statement, handlerName, context);
            toReturn.Add(getNextIndexEpression.ToStatementSyntax());


            forStatementSyntax = forStatementSyntax.WithCondition( condition )
                .WithIncrementors( getNextIndexEpression.ToSeparatedSyntaxList<ExpressionSyntax>() );

            toReturn.Add( forStatementSyntax );

            toReturn.Add( SyntaxFactory.LabeledStatement( exitLoopLabelName, SyntaxFactory.EmptyStatement() ) );
            context.UnregisterLoop();

            return toReturn;
        }

        private static LocalDeclarationStatementSyntax GetIndexLocalDeclaration(
            ForToNextStatement node,
            VariableDeclarationSyntax indexVariableDeclaration, IContextService context ) {
            return SyntaxFactory.LocalDeclarationStatement(
                indexVariableDeclaration.WithVariables(
                    SyntaxFactory.VariableDeclarator( context.GetVariableName(node.VariableExpression.IdentifierName) )
                        .WithInitializer( SyntaxFactory.EqualsValueClause( SyntaxFactory.LiteralExpression( SyntaxKind.NullLiteralExpression ) ) )
                        .ToSeparatedSyntaxList() ) );
        }

        private ForStatementSyntax CreateForStatement( ForToNextStatement node, IContextService context, string continueCaseLabelName ) {
            List<StatementSyntax> block = node.Block.SelectMany( x => ConvertGeneric( x, context ) ).ToList();
            block.Add( SyntaxFactory.LabeledStatement( continueCaseLabelName, SyntaxFactory.EmptyStatement() ) );
            return
                SyntaxFactory.ForStatement( block.ToBlock() );
        }

        private static BinaryExpressionSyntax GetNextIndexExpression( ForToNextStatement node, string handlerName, IContextService context ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.IdentifierName(
                    context.GetVariableName(node.VariableExpression.IdentifierName) ),
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

        private static VariableDeclarationSyntax GetIndexVariableDeclaration( ForToNextStatement node, IContextService context ) {
            return SyntaxFactory.VariableDeclaration(
                SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                SyntaxFactory.VariableDeclarator( context.GetVariableName(node.VariableExpression.IdentifierName) ).ToSeparatedSyntaxList() );
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
                        SyntaxFactory.SeparatedList(
                            new[] {
                                SyntaxFactory.Argument( Convert( node.StartExpression, context ) ),
                                SyntaxFactory.Argument( Convert( node.EndExpression, context ) ),
                                SyntaxFactory.Argument(
                                    node.StepExpression != null
                                        ? Convert( node.StepExpression, context )
                                        : SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NullLiteralExpression ) )
                            } ) ) );
        }
    }
}
