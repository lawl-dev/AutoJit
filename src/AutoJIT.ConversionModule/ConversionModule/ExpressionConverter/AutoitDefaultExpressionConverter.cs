using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitDefaultExpressionConverter : AutoitExpressionConverterBase<DefaultExpression>
    {
        public AutoitDefaultExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( DefaultExpression node, IContextService contextService ) {
            IdentifierNameSyntax typeName = SyntaxFactory.IdentifierName( typeof (Variant).Name );
            string variantCreateName = CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, typeName, SyntaxFactory.IdentifierName( variantCreateName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( SyntaxFactory.ObjectCreationExpression( SyntaxFactory.IdentifierName( typeof (Default).Name ) ) ).ToSeparatedSyntaxList() ) );
        }
    }
}
