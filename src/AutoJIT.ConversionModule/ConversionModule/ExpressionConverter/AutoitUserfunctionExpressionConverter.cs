using AutoJIT.Contrib;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Parser.AST.Expressions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitUserfunctionExpressionConverter : AutoitExpressionConverterBase<UserfunctionExpression>
    {
        public AutoitUserfunctionExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( UserfunctionExpression node, IContextService contextService ) {
            string createFunctionName = CompilerHelper.GetVariantMemberName( x => Variant.CreateFunction( null, null ) );

            ArgumentSyntax @this = SyntaxFactory.Argument( SyntaxFactory.ThisExpression() );
            ArgumentSyntax funcName = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(node.IdentifierName.Token.Value.StringValue, node.IdentifierName.Token.Value.StringValue)));

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( createFunctionName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList().AddArguments( @this, funcName ) );
        }
    }
}
