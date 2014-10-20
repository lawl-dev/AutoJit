using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Service;
using AutoJITRuntime;
using AutoJITRuntime.Variants;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitDefaultExpressionConverter : AutoitExpressionConverterBase<DefaultExpression>
    {
        public AutoitDefaultExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( DefaultExpression node, IContextService context ) {
            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ),
                    SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) ).
                WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.Argument( SyntaxFactory.ObjectCreationExpression( SyntaxFactory.IdentifierName( typeof (Default).Name ) ) )
                            .ToSeparatedSyntaxList() ) );
        }
    }
}
