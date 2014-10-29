using System;
using AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter.Interface;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal abstract class AutoitExpressionConverterBase<TExpression> : IAutoitExpressionConverter<TExpression, ExpressionSyntax>
    where TExpression : IExpressionNode
    {
        private readonly IInjectionService _injectionService;

        protected AutoitExpressionConverterBase( IInjectionService injectionService ) {
            _injectionService = injectionService;
        }

        public abstract ExpressionSyntax Convert( TExpression node, IContextService context );

        public ExpressionSyntax ConverGeneric( IExpressionNode node, IContextService contextService ) {
            return GetConverter( node ).Convert( node, contextService );
        }

        public ExpressionSyntax Convert( IExpressionNode node, IContextService contextService ) {
            return Convert( (TExpression)node, contextService );
        }

        public ExpressionSyntax Convert<TNode>( IExpressionNode node, IContextService contextService ) {
            return GetConverter<TNode>().Convert( node, contextService );
        }

        private IAutoitExpressionConverter<ExpressionSyntax> GetConverter( IExpressionNode node ) {
            Type converterType = typeof(IAutoitExpressionConverter<,>).MakeGenericType( node.GetType(), typeof(ExpressionSyntax) );
            return _injectionService.Inject<IAutoitExpressionConverter<ExpressionSyntax>>( converterType );
        }

        private IAutoitExpressionConverter<ExpressionSyntax> GetConverter<TNode>() {
            Type converterType = typeof(IAutoitExpressionConverter<,>).MakeGenericType( typeof(TNode), typeof(ExpressionSyntax) );
            return _injectionService.Inject<IAutoitExpressionConverter<ExpressionSyntax>>( converterType );
        }
    }
}
