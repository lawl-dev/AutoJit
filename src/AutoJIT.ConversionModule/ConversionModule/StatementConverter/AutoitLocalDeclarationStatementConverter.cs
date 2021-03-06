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
    internal class AutoitLocalDeclarationStatementConverter : AutoitStatementConverterBase<LocalDeclarationStatement>
    {
        public AutoitLocalDeclarationStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( LocalDeclarationStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            if ( !context.IsDeclaredLocal( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ) {
                context.RegisterLocal( statement.VariableExpression.IdentifierName.Token.Value.StringValue );
                toReturn.Add( DeclareLocal( statement, context ) );
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
            return CSharpStatementFactory.CreateInvocationExpression( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ), CompilerHelper.GetVariantMemberName( x => x.InitArray( null ) ), new CSharpParameterInfo( ConvertGeneric( statement.InitExpression, contextService ), false ).ToEnumerable() ).ToStatementSyntax();
        }

        private StatementSyntax DeclareLocal( LocalDeclarationStatement statement, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( statement, context );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( LocalDeclarationStatement statement, IContextService context ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable( typeof (Variant).Name, context.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue, Scope.Local ) );
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( LocalDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), ConvertGeneric( statement.InitExpression, contextService ) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( LocalDeclarationStatement statement, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName( contextService.GetVariableName( statement.VariableExpression.IdentifierName.Token.Value.StringValue ) ), GetArrayInitExpression( statement, contextService ) ).ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( LocalDeclarationStatement statement, IContextService contextService ) {
            SeparatedSyntaxList<ExpressionSyntax> openBracketToken = ( (ArrayExpression) statement.VariableExpression ).AccessParameter.Select( x => ConvertGeneric( x, contextService ) ).ToSeparatedSyntaxList();
            ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression( SyntaxFactory.ArrayType( SyntaxFactory.IdentifierName( typeof (Variant).Name ) ).WithRankSpecifiers( SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.CreateArray( null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( arrayCreationExpressionSyntax ).ToSeparatedSyntaxList() ) );
        }
    }
}
