using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.CSharpConverter.ConversionModule.Helper
{
    public static class StatementSyntaxExtensions
    {
        public static ElseClauseSyntax ToElseClause( this StatementSyntax src ) {
            return SyntaxFactory.ElseClause( src );
        }

        public static BlockSyntax ToBlock( this IEnumerable<StatementSyntax> stc ) {
            return SyntaxFactory.Block( stc );
        }
    }
}
