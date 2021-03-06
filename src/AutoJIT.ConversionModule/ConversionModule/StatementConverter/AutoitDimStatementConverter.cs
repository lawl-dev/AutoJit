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
    internal sealed class AutoitDimStatementConverter : AutoitStatementConverterBase<DimStatement>
    {
        public AutoitDimStatementConverter( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) : base( cSharpStatementFactory, injectionService ) {}

        public override IEnumerable<StatementSyntax> Convert( DimStatement statement, IContextService context ) {
            var toReturn = new List<StatementSyntax>();

            if ( context.GetIsGlobalContext() ) {
                context.RegisterGlobal(statement.VariableExpression.IdentifierName.Token.Value.StringValue);
                context.PushGlobalVariable(statement.VariableExpression.IdentifierName.Token.Value.StringValue, DeclareGlobal(statement, context));
            }
            else {
                if (!context.IsDeclaredLocal(statement.VariableExpression.IdentifierName.Token.Value.StringValue))
                {
                    context.RegisterLocal(statement.VariableExpression.IdentifierName.Token.Value.StringValue);
                    toReturn.Add( DeclareLocal( statement, context ) );
                }
            }

            if ( statement.VariableExpression is ArrayExpression ) {
                toReturn.Add( InitArray( statement, context ) );
            }
            if ( statement.InitExpression != null ) {
                toReturn.Add( AssignVariable( statement, context ) );
            }
            return toReturn;
        }

        private FieldDeclarationSyntax DeclareGlobal( DimStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateFieldDeclarationStatement( variableDeclarationSyntax );
        }

        private StatementSyntax DeclareLocal( DimStatement node, IContextService context ) {
            VariableDeclarationSyntax variableDeclarationSyntax = DeclareVariable( node, context );
            return CSharpStatementFactory.CreateLocalDeclarationStatement( variableDeclarationSyntax );
        }

        private VariableDeclarationSyntax DeclareVariable( DimStatement node, IContextService context ) {
            VariableDeclarationSyntax declarationSyntax = CSharpStatementFactory.CreateVariable(typeof(Variant).Name, context.GetVariableName(node.VariableExpression.IdentifierName.Token.Value.StringValue));
            return declarationSyntax;
        }

        private StatementSyntax AssignVariable( DimStatement node, IContextService contextService ) {
            return SyntaxFactory.BinaryExpression( SyntaxKind.SimpleAssignmentExpression, ConvertGeneric( node.VariableExpression, contextService ), ConvertGeneric( node.InitExpression, contextService ) ).ToStatementSyntax();
        }

        private StatementSyntax InitArray( DimStatement node, IContextService context ) {
            return SyntaxFactory.BinaryExpression(SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName(context.GetVariableName(node.VariableExpression.IdentifierName.Token.Value.StringValue)), GetArrayInitExpression(node, context)).ToStatementSyntax();
        }

        private ExpressionSyntax GetArrayInitExpression( DimStatement node, IContextService context ) {
            SeparatedSyntaxList<ExpressionSyntax> openBracketToken = ( (ArrayExpression) node.VariableExpression ).AccessParameter.Select( x => ConvertGeneric( x, context ) ).ToSeparatedSyntaxList();
            ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression( SyntaxFactory.ArrayType( SyntaxFactory.IdentifierName( typeof (Variant).Name ) ).WithRankSpecifiers( SyntaxFactory.ArrayRankSpecifier( openBracketToken ).ToEnumerable().ToSyntaxList() ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.CreateArray( null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( arrayCreationExpressionSyntax ).ToSeparatedSyntaxList() ) );
        }
    }
}
