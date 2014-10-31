using System;
using System.Collections.Generic;
using AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter.Interface;
using AutoJIT.CSharpConverter.ConversionModule.Factory;
using AutoJIT.CSharpConverter.ConversionModule.StatementConverter.Interface;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.StatementConverter
{
	internal abstract class AutoitStatementConverterBase<TStatement> : IAutoitStatementConverter<TStatement, StatementSyntax>
	where TStatement : IStatementNode
	{
		protected readonly ICSharpStatementFactory CSharpStatementFactory;
		private readonly IInjectionService _injectionService;

		protected AutoitStatementConverterBase( ICSharpStatementFactory cSharpStatementFactory, IInjectionService injectionService ) {
			CSharpStatementFactory = cSharpStatementFactory;
			_injectionService = injectionService;
		}

		public abstract IEnumerable<StatementSyntax> Convert( TStatement statement, IContextService context );

		public IEnumerable<StatementSyntax> ConvertGeneric( IStatementNode node, IContextService contextService ) {
			return GetConverter( node ).Convert( node, contextService );
		}

		public IEnumerable<StatementSyntax> Convert( IStatementNode node, IContextService contextService ) {
			return Convert( (TStatement)node, contextService );
		}

		protected ExpressionSyntax Convert( IExpressionNode node, IContextService contextService ) {
			return GetConverter( node ).ConvertGeneric( node, contextService );
		}

		private IAutoitExpressionConverter<ExpressionSyntax> GetConverter( IExpressionNode node ) {
			Type converterType = typeof(IAutoitExpressionConverter<,>).MakeGenericType( node.GetType(), typeof(ExpressionSyntax) );
			return _injectionService.Inject<IAutoitExpressionConverter<ExpressionSyntax>>( converterType );
		}

		private IAutoitStatementConverter<StatementSyntax> GetConverter( IStatementNode node ) {
			Type converterType = typeof(IAutoitStatementConverter<,>).MakeGenericType( node.GetType(), typeof(StatementSyntax) );
			return _injectionService.Inject<IAutoitStatementConverter<StatementSyntax>>( converterType );
		}
	}
}
