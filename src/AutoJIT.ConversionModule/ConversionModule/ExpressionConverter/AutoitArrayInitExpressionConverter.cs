using System.Linq;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitArrayInitExpressionConverter : AutoitExpressionConverterBase<ArrayInitExpression>
    {
        public AutoitArrayInitExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( ArrayInitExpression node, IContextService context ) {
            ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = SyntaxFactory.ArrayRankSpecifier(
                SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                    SyntaxFactory.OmittedArraySizeExpression() ) );

            ArrayCreationExpressionSyntax arrayCreationExpression =
                SyntaxFactory.ArrayCreationExpression(
                    SyntaxFactory.ArrayType( SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.SingletonList( arrayRankSpecifierSyntax ) ) );

            return arrayCreationExpression.WithInitializer(
                SyntaxFactory.InitializerExpression(
                    SyntaxKind.ArrayInitializerExpression,
                    node.ToAssign.Select( x => ConverGeneric( x, context ) ).ToSeparatedSyntaxList() ) );
        }
    }
}
