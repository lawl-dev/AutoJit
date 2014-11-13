using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitStringLiteralExpressionConverter : AutoitExpressionConverterBase<StringLiteralExpression>
    {
        public AutoitStringLiteralExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( StringLiteralExpression node, IContextService contextService ) {
            LiteralExpressionSyntax expression = SyntaxFactory.LiteralExpression( SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal( node.LiteralToken.Token.Value.StringValue ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( expression ).ToSeparatedSyntaxList() ) );
        }
    }
}
