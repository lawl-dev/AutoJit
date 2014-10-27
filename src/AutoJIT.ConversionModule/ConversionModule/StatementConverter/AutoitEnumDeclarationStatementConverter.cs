using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitEnumDeclarationStatementConverter : AutoitStatementConverterBase<EnumDeclarationStatement>
    {
        public AutoitEnumDeclarationStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( EnumDeclarationStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            Scope scope;
            if( statement is GlobalEnumDeclarationStatement ) {
                scope = Scope.Global;
                context.DeclareGlobal( statement.VariableExpression.IdentifierName );
                context.PushGlobalVariable( context.GetVariableName( statement.VariableExpression.IdentifierName, Scope.Global ), DeclareGlobal( statement, context ) );
            }
            else {
                scope = Scope.Local;
                context.DeclareLocal( statement.VariableExpression.IdentifierName );
                toReturn.Add( DeclareLocal( statement, context ) );
            }

            if( statement.UserInitExpression != null ) {
                toReturn.Add( AssignVariable( statement, context, scope ) );
            }
            else {
                toReturn.Add( SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( context.GetVariableName( statement.VariableExpression.IdentifierName, scope ) ), Convert( statement.AutoInitExpression, context ) ).ToStatementSyntax() );
            }
            return toReturn;
        }

        private FieldDeclarationSyntax DeclareGlobal( EnumDeclarationStatement statement, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( statement, context, Scope.Global );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private StatementSyntax DeclareLocal( EnumDeclarationStatement statement, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( statement, context, Scope.Local );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( EnumDeclarationStatement statement, IContextService context, Scope scope ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable( typeof(Variant).Name, context.GetVariableName( statement.VariableExpression.IdentifierName, scope ) );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( EnumDeclarationStatement statement, IContextService context, Scope scope ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( context.GetVariableName( statement.VariableExpression.IdentifierName, scope ) ), Convert( statement.UserInitExpression, context ) ).ToStatementSyntax();
        }
    }
}
