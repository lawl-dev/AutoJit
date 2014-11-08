using AutoJIT.CSharpConverter.ConversionModule.Helper;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Lex;
using AutoJITRuntime;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.ExpressionConverter
{
    internal sealed class AutoitNumericLiteralExpressionConverter : AutoitExpressionConverterBase<NumericLiteralExpression>
    {
        public AutoitNumericLiteralExpressionConverter( IInjectionService injectionService ) : base( injectionService ) {}

        public override ExpressionSyntax Convert( NumericLiteralExpression node, IContextService contextService ) {
            ExpressionSyntax expression = null;

            switch (node.LiteralToken.Token.Type) {
                case TokenType.Int32:
                    expression = GetIntLiteralExpression( node );
                    break;
                case TokenType.Int64:
                    expression = GetInt64LiteralExpression( node );
                    break;
                case TokenType.Double:
                    expression = GetDoubleLiteralExpression( node );
                    break;
            }

            return SyntaxFactory.InvocationExpression( SyntaxFactory.MemberAccessExpression( SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName( typeof (Variant).Name ), SyntaxFactory.IdentifierName( CompilerHelper.GetVariantMemberName( x => Variant.Create( (object) null ) ) ) ) ).WithArgumentList( SyntaxFactory.ArgumentList( SyntaxFactory.Argument( expression ).ToSeparatedSyntaxList() ) );
        }

        private ExpressionSyntax GetDoubleLiteralExpression( NumericLiteralExpression node ) {
            double value = node.LiteralToken.Token.Value.DoubleValue;
            if ( node.Negativ ) {
                value = -value;
            }
            return SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( value ) );
        }

        private ExpressionSyntax GetInt64LiteralExpression( NumericLiteralExpression node ) {
            long value = node.LiteralToken.Token.Value.Int64Value;
            if ( node.Negativ ) {
                value = -value;
            }
            return SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( value ) );
        }

        private ExpressionSyntax GetIntLiteralExpression( NumericLiteralExpression node ) {
            int value = node.LiteralToken.Token.Value.Int32Value;
            if ( node.Negativ ) {
                value = -value;
            }
            return SyntaxFactory.LiteralExpression( SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal( value ) );
        }
    }
}
