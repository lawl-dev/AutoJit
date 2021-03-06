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
    internal sealed class AutoitGlobalDeclarationStatementConverter : AutoitStatementConverterBase<GlobalDeclarationStatement>
    {
        public AutoitGlobalDeclarationStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( GlobalDeclarationStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();
            if ( !context.IsDeclaredGlobal( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ) {
                context.RegisterGlobal( statement.VariableExpression.IdentifierName.Token.Value.StringValue );
                context.PushGlobalVariable( statement.VariableExpression.IdentifierName.Token.Value.StringValue, DeclareGlobal( statement, context ) );
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

        private StatementSyntax AssignArray( GlobalDeclarationStatement statement, IContextService context ) {
            return CSharpStatementFactory.CreateInvocationExpression( context.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue, Scope.Global ), CompilerHelper.GetVariantMemberName( x => x.InitArray( null ) ), new CSharpParameterInfo( ConvertGeneric( statement.InitExpression, context ), false ).ToEnumerable() ).ToStatementSyntax();
        }

        private FieldDeclarationSyntax DeclareGlobal( GlobalDeclarationStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( GlobalDeclarationStatement node, IContextService context ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, context.GetVariableName( node.VariableExpression.IdentifierName.Token.Value.StringValue, Scope.Global ) );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( GlobalDeclarationStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( context.GetVariableName( node.VariableExpression.IdentifierName.Token.Value.StringValue, Scope.Global ) ), ConvertGeneric( node.InitExpression, context ) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( GlobalDeclarationStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( context.GetVariableName( node.VariableExpression.IdentifierName.Token.Value.StringValue, Scope.Global ) ), GetArrayInitExpression( node, context ) ).ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( GlobalDeclarationStatement node, IContextService context ) {
            SeparatedSyntaxList<ExpressionSyntax> openBracketToken = ( (ArrayExpression) node.VariableExpression ).AccessParameter.Select( x => ConvertGeneric( x, context ) ).ToSeparatedSyntaxList();
            ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression( SyntaxFactory.ArrayType( SyntaxFactory.IdentifierName( typeof (Variant).Name ) ).WithRankSpecifiers( SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.CreateArray( null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( arrayCreationExpressionSyntax ).ToSeparatedSyntaxList() ) );
        }
    }
}
