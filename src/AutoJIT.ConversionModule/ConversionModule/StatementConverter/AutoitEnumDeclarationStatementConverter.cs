using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Exceptions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitEnumDeclarationStatementConverter : AutoitStatementConverterBase<EnumDeclarationStatement>
    {
        public AutoitEnumDeclarationStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService)
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert(EnumDeclarationStatement statement, IContextService context)
        {
            var toReturn = new List<StatementSyntax>();

            if ( context.IsDeclared( statement.VariableExpression.IdentifierName ) ) {
                throw new SyntaxTreeException( string.Format( "{0} is still declared", statement.VariableExpression.IdentifierName ), 0, 0 );
            }

            if ( statement.IsGlobal ) {
                context.PushGlobalVariable( statement.VariableExpression.IdentifierName, DeclareGlobal( statement ) );
            }
            else {
                context.Declare( statement.VariableExpression.IdentifierName );
                toReturn.Add( DeclareLocal( statement ) );
            }

            if ( statement.UserInitExpression != null ) {
                toReturn.Add( AssignVariable( statement, context ) );
            }
            else {
                toReturn.Add(
                    SyntaxFactory.BinaryExpression(
                        SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( statement.VariableExpression.IdentifierName ),
                        Convert(statement.AutoInitExpression, context) ).ToStatementSyntax() );
            }
            return toReturn;
        }

        private FieldDeclarationSyntax DeclareGlobal( EnumDeclarationStatement statement ) {
            var variableDeclarationSyntax = DeclareVariable( statement );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private StatementSyntax DeclareLocal( EnumDeclarationStatement statement ) {
            var variableDeclarationSyntax = DeclareVariable( statement );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( EnumDeclarationStatement statement ) {
            var declarationSyntax = CSharpStatementFactory.CreateVariable(
                typeof (Variant).Name, statement.VariableExpression.IdentifierName );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( EnumDeclarationStatement statement, IContextService context ) {
            return SyntaxFactory.BinaryExpression(
                SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( statement.VariableExpression.IdentifierName ),
                Convert(statement.UserInitExpression, context) ).ToStatementSyntax();
        }
    }
}
