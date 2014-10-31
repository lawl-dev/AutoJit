using System.Collections.Generic;
using System.Linq;
using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
	internal sealed class AutoitVariableFunctionCallExpressionConverter : AutoitInvocationExpressionConverterBase<VariableFunctionCallExpression>
	{
		public AutoitVariableFunctionCallExpressionConverter(IInjectionService injectionService) : base(injectionService) { }

		public override ExpressionSyntax Convert(VariableFunctionCallExpression node, IContextService contextService)
		{
			IEnumerable<ArgumentSyntax> parameter = CreateParameter(node.Parameter, contextService);

			var invokeName = CompilerHelper.GetVariantMemberName( x=>x.Invoke(  ) );


			return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, ConvertGeneric( node.VariableExpression, contextService ), SyntaxFactory.IdentifierName( invokeName ) ) ).WithArgumentList( SyntaxFactory.ArgumentList().AddArguments( parameter.ToArray() ) );
		}
	}
}