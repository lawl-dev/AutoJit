using System;
using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
    internal sealed class AutoitAssignStatementConverter : AutoitStatementConverterBase<AssignStatement>
    {
        public AutoitAssignStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( AssignStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            if ( !context.IsDeclared( statement.Variable.IdentifierName ) ) {
                if ( context.GetIsGlobalContext() ) {
                    context.RegisterGlobal( statement.Variable.IdentifierName );
                    context.PushGlobalVariable( statement.Variable.IdentifierName, DeclareGlobal( statement, context ) );
                }
                else {
                    context.RegisterLocal( statement.Variable.IdentifierName );
                    toReturn.Add( DeclareLocal( statement, context ) );
                }
            }

            toReturn.Add( AssignVariable( statement, context ) );

            return toReturn;
        }

        private FieldDeclarationSyntax DeclareGlobal( AssignStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private StatementSyntax DeclareLocal( AssignStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( AssignStatement node, IContextService context ) {
            return DeclareVariable( node.Variable.IdentifierName, context );
        }

        private VariableDeclarationSyntax DeclareVariable( string identifierName, IContextService context ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, context.GetVariableName( identifierName ) );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( AssignStatement node, IContextService context ) {
            ExpressionSyntax toAssign;
            SyntaxKind kind;
            switch (node.Operator.Token.Type) {
                case TokenType.PowAssign:
                    kind = SyntaxKind.SimpleAssignmentExpression;
                    toAssign = CSharpStatementFactory.CreateInvocationExpression( context.GetVariableName( node.Variable.IdentifierName ), CompilerHelper.GetVariantMemberName( x => x.PowAssign( null ) ), new CSharpParameterInfo( ConvertGeneric( node.ExpressionToAssign, context ), false ).ToEnumerable() );
                    break;
                case TokenType.ConcatAssign:
                    kind = SyntaxKind.SimpleAssignmentExpression;
                    toAssign = CSharpStatementFactory.CreateInvocationExpression( context.GetVariableName( node.Variable.IdentifierName ), CompilerHelper.GetVariantMemberName( x => x.ConcatAssign( null ) ), new CSharpParameterInfo( ConvertGeneric( node.ExpressionToAssign, context ), false ).ToEnumerable() );
                    break;
                default:
                    kind = GetAssignOperatorKind( node.Operator.Token );
                    toAssign = ConvertGeneric( node.ExpressionToAssign, context );
                    break;
            }
            return SyntaxFactory.BinaryExpression( kind, ConvertGeneric( node.Variable, context ), toAssign ).ToStatementSyntax();
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
