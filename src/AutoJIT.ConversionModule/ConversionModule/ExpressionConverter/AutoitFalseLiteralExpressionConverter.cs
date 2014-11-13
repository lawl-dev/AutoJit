using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitFalseLiteralExpressionConverter : AutoitExpressionConverterBase<FalseLiteralExpression>
    {
        public AutoitFalseLiteralExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( FalseLiteralExpression node, IContextService contextService ) {
            {
                LiteralExpressionSyntax falseExpression = SyntaxFactory.LiteralExpression( SyntaxKind.FalseLiteralExpression );
                IdentifierNameSyntax typeName = SyntaxFactory.IdentifierName( typeof (Variant).Name );
                string variantCreateName = CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) );

                return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, typeName, SyntaxFactory.IdentifierName( variantCreateName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( falseExpression ).ToSeparatedSyntaxList() ) );
            }
        }
    }
}
