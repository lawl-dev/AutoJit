using System.Collections.Generic;
using System.Linq;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
	internal sealed class AutoitArrayInitExpressionConverter : AutoitExpressionConverterBase<ArrayInitExpression>
	{
		public AutoitArrayInitExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

		public override ExpressionSyntax Convert( ArrayInitExpression node, IContextService contextService ) {
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = SyntaxFactory.ArrayRankSpecifier( SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>( SyntaxFactory.OmittedArraySizeExpression() ) );

			IdentifierNameSyntax arrayType = SyntaxFactory.IdentifierName( typeof(Variant).Name );

			ArrayCreationExpressionSyntax arrayCreationExpression = SyntaxFactory.ArrayCreationExpression( SyntaxFactory.ArrayType( arrayType, SyntaxFactory.SingletonList( arrayRankSpecifierSyntax ) ) );

			IEnumerable<ExpressionSyntax> expressionToAssign = node.ToAssign.Select( x => ConverGeneric( x, contextService ) );

			return arrayCreationExpression.WithInitializer( SyntaxFactory.InitializerExpression( SyntaxKind.ArrayInitializerExpression, expressionToAssign.ToSeparatedSyntaxList() ) );
		}
	}
}
