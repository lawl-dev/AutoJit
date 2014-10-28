using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitNullExpressionConverter : AutoitExpressionConverterBase<NullExpression>
    {
        public AutoitNullExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( NullExpression node, IContextService context ) {
            return
            SyntaxFactory.InvocationExpression(
                                               SyntaxFactory.MemberAccessExpression(
                                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                                    SyntaxFactory.IdentifierName( typeof(Variant).Name ),
                                                                                    SyntaxFactory.IdentifierName(
                                                                                                                 CompilerHelper.GetVariantMemberName(
                                                                                                                                                     x =>
                                                                                                                                                     Variant
                                                                                                                                                     .Create(
                                                                                                                                                             (
                                                                                                                                                             object
                                                                                                                                                             )
                                                                                                                                                             null ) ) ) ) )
                         .WithArgumentList(
                                           SyntaxFactory.ArgumentList(
                                                                      SyntaxFactory.Argument(
                                                                                             SyntaxFactory.CastExpression(
                                                                                                                          SyntaxFactory.PredefinedType(
                                                                                                                                                       SyntaxFactory
                                                                                                                                                       .Token(
                                                                                                                                                              SyntaxKind
                                                                                                                                                              .ObjectKeyword ) ),
                                                                                                                          SyntaxFactory.LiteralExpression(
                                                                                                                                                          SyntaxKind
                                                                                                                                                          .NullLiteralExpression ) ) )
                                                                                   .ToSeparatedSyntaxList() ) );
        }
    }
}
