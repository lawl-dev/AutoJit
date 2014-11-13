using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitTrueLiteralExpressionConverter : AutoitExpressionConverterBase<TrueLiteralExpression>
    {
        public AutoitTrueLiteralExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( TrueLiteralExpression node, IContextService contextService ) {
            {
                LiteralExpressionSyntax expression = SyntaxFactory.LiteralExpression( SyntaxKind.TrueLiteralExpression );

                return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( expression ).ToSeparatedSyntaxList() ) );
            }
        }
    }
}
