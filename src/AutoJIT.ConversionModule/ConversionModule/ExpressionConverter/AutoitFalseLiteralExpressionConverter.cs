using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitFalseLiteralExpressionConverter : AutoitExpressionConverterBase<FalseLiteralExpression>
    {
        public AutoitFalseLiteralExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( FalseLiteralExpression node, IContextService context ) {
            {
                var expression = SyntaxFactory.LiteralExpression( SyntaxKind.FalseLiteralExpression );
                return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                        SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) )
                    .WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( expression ).ToSeparatedSyntaxList() ) );
            }
        }
    }
}
