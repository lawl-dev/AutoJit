using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Service;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitVariableExpressionConverter : AutoitExpressionConverterBase<VariableExpression>
    {
        public AutoitVariableExpressionConverter( IInjectionService injectionService )
            : base( injectionService ) {}

        public override ExpressionSyntax Convert( VariableExpression node, IContextService context ) {
            return SyntaxFactory.IdentifierName( node.IdentifierName );
        }
    }
}
