using System;
using System.Collections.Generic;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitAssignStatementConverter : AutoitStatementConverterBase<AssignStatement>
    {
        public AutoitAssignStatementConverter(
            ICSharpStatementFactory cSharpStatementFactory,
            IInjectionService injectionService )
            : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( AssignStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            if ( !context.IsDeclared( statement.Variable.IdentifierName ) ) {
                if ( context.GetIsGlobalContext() ) {
                    context.PushGlobalVariable( statement.Variable.IdentifierName, DeclareGlobal( statement ) );
                }
                else {
                    context.Declare( statement.Variable.IdentifierName );
                    toReturn.Add( DeclareLocal( statement ) );
                }
            }
            toReturn.Add( AssignVariable( statement, context ) );
            return toReturn;
        }

        private FieldDeclarationSyntax DeclareGlobal( AssignStatement node ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private StatementSyntax DeclareLocal( AssignStatement node ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( AssignStatement node ) {
            return DeclareVariable( node.Variable.IdentifierName );
        }

        private VariableDeclarationSyntax DeclareVariable( string identifierName ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, identifierName );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( AssignStatement node, IContextService context ) {
            ExpressionSyntax toAssign;
            SyntaxKind kind;
            switch (node.Operator.Type) {
                case TokenType.PowAssign:
                    kind = SyntaxKind.SimpleAssignmentExpression;
                    toAssign = CSharpStatementFactory.CreateInvocationExpression(
                        node.Variable.IdentifierName, CompilerHelper.GetVariantMemberName( x => x.PowAssign( null ) ),
                        new CSharpParameterInfo( Convert( node.ExpressionToAssign, context ), false )
                            .ToEnumerable() );
                    break;
                case TokenType.ConcatAssign:
                    kind = SyntaxKind.SimpleAssignmentExpression;
                    toAssign = CSharpStatementFactory.CreateInvocationExpression(
                        node.Variable.IdentifierName, CompilerHelper.GetVariantMemberName( x => x.ConcatAssign( null ) ),
                        new CSharpParameterInfo( Convert( node.ExpressionToAssign, context ), false )
                            .ToEnumerable() );
                    break;
                default:
                    kind = GetAssignOperatorKind( node.Operator );
                    toAssign = Convert( node.ExpressionToAssign, context );
                    break;
            }
            return SyntaxFactory.BinaryExpression(
                kind, Convert( node.Variable, context ),
                toAssign ).ToStatementSyntax();
        }

        private SyntaxKind GetAssignOperatorKind( Token assignOperator ) {
            switch (assignOperator.Type) {
                case TokenType.DivAssign:
                    return SyntaxKind.DivideAssignmentExpression;
                case TokenType.MinusAssign:
                    return SyntaxKind.SubtractAssignmentExpression;
                case TokenType.MultAssign:
                    return SyntaxKind.MultiplyAssignmentExpression;
                case TokenType.PlusAssign:
                    return SyntaxKind.AddAssignmentExpression;
                case TokenType.Equal:
                    return SyntaxKind.SimpleAssignmentExpression;
                default:
                    throw new NotImplementedException( assignOperator.Type.ToString() );
            }
        }
    }
}
