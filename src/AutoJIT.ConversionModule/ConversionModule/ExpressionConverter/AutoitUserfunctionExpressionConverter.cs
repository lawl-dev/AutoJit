using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
	internal sealed class AutoitUserfunctionExpressionConverter : AutoitExpressionConverterBase<UserfunctionExpression>
	{
		public AutoitUserfunctionExpressionConverter(IInjectionService injectionService) : base(injectionService) { }

		public override ExpressionSyntax Convert(UserfunctionExpression node, IContextService contextService)
		{
			var createFunctionName = CompilerHelper.GetVariantMemberName(x => Variant.CreateFunction(null, null));

			var @this = SyntaxFactory.Argument(SyntaxFactory.ThisExpression());
			var funcName = SyntaxFactory.Argument( SyntaxFactory.LiteralExpression( SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal( node.IdentifierName, node.IdentifierName ) ) );


			return SyntaxFactory.InvocationExpression(SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(typeof(Variant).Name), SyntaxFactory.IdentifierName(createFunctionName))).WithArgumentList(SyntaxFactory.ArgumentList().AddArguments(@this, funcName));
		}
	}
}