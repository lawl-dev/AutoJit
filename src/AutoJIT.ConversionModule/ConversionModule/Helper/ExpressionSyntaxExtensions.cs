using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Helper
{
    public static class ExpressionSyntaxExtensions
    {
        public static StatementSyntax ToStatementSyntax( this ExpressionSyntax src ) {
            return SyntaxFactory.ExpressionStatement( src );
        }
    }
}
