﻿using System.Collections.Generic;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitArrayExpressionConverter : AutoitExpressionConverterBase<ArrayExpression>
    {
        public AutoitArrayExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( ArrayExpression node, IContextService contextService ) {
            ExpressionSyntax variable = Convert<VariableExpression>( node, contextService );

            return AddElementAccessExpression( node, contextService, variable );
        }

        private ElementAccessExpressionSyntax AddElementAccessExpression( ArrayExpression node, IContextService context, ExpressionSyntax variable ) {
            BracketedArgumentListSyntax argumentList = SyntaxFactory.BracketedArgumentList( SyntaxFactory.SeparatedList<ArgumentSyntax>().AddRange( CreateArguments( node, context ) ) );

            return SyntaxFactory.ElementAccessExpression( variable, argumentList );
        }

        private IEnumerable<ArgumentSyntax> CreateArguments( ArrayExpression node, IContextService context ) {
            return node.AccessParameter.Select( x => SyntaxFactory.Argument( ConvertGeneric( x, context ) ) );
        }
    }
}
