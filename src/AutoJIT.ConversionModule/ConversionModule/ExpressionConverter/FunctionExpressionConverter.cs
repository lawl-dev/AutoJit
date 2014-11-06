using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitFunctionExpressionConverter : AutoitExpressionConverterBase<FunctionExpression>
    {
        public AutoitFunctionExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( FunctionExpression node, IContextService contextService ) {
            string createFunctionName = CompilerHelper.GetVariantMemberName( x => Variant.CreateFunction( null, null ) );

            ArgumentSyntax runtimeName = SyntaxFactory.Argument( SyntaxFactory.IdentifierName( contextService.GetRuntimeInstanceName() ) );
            ArgumentSyntax funcName = SyntaxFactory.Argument( SyntaxFactory.LiteralExpression( SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal( node.IdentifierName, node.IdentifierName ) ) );

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( createFunctionName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList().AddArguments( runtimeName, funcName ) );
        }
    }
}
