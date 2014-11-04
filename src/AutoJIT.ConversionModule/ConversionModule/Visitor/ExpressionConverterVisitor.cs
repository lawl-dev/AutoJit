using System;
using AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter.Interface;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CSharp.RuntimeBinder;

namespace AutoJIT.CSharpConverter.ConversionModule.Visitor
{
	public class ExpressionConverterVisitor
	{
		private readonly IInjectionService _injectionService;
		private IContextService _contextService;

		public ExpressionConverterVisitor( IInjectionService injectionService ) {
			_injectionService = injectionService;
		}

		public ExpressionSyntax Visit( IExpressionNode node ) {
			try {
				return Visit( (dynamic)node );
			}
			catch(RuntimeBinderException ex) {
				throw new NotImplementedException();
			}
		}

		public void Initialize( IContextService contextService ) {
			_contextService = contextService;
		}

		private ExpressionSyntax Visit( BinaryExpression node ) {
			return GetConverter<BinaryExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( ArrayExpression node ) {
			return GetConverter<ArrayExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( ArrayInitExpression node ) {
			return GetConverter<ArrayInitExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( BooleanNegateExpression node ) {
			return GetConverter<BooleanNegateExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( CallExpression node ) {
			return GetConverter<CallExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( DefaultExpression node ) {
			return GetConverter<DefaultExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( FalseLiteralExpression node ) {
			return GetConverter<FalseLiteralExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( MacroExpression node ) {
			return GetConverter<MacroExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( NegateExpression node ) {
			return GetConverter<NegateExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( NullExpression node ) {
			return GetConverter<NullExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( NumericLiteralExpression node ) {
			return GetConverter<NumericLiteralExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( StringLiteralExpression node ) {
			return GetConverter<StringLiteralExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( TernaryExpression node ) {
			return GetConverter<TernaryExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( TrueLiteralExpression node ) {
			return GetConverter<TrueLiteralExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( UserfunctionCallExpression node ) {
			return GetConverter<UserfunctionCallExpression>().Convert( node, _contextService );
		}

		private ExpressionSyntax Visit( VariableExpression node ) {
			return GetConverter<VariableExpression>().Convert( node, _contextService );
		}

		private IAutoitExpressionConverter<T, ExpressionSyntax> GetConverter<T>() where T : IExpressionNode {
			return _injectionService.Inject<IAutoitExpressionConverter<T, ExpressionSyntax>>();
		}
	}
}
