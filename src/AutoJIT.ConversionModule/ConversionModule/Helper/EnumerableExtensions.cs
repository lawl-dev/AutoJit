using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AutoJIT.CSharpConverter.ConversionModule.Helper
{
    public static class EnumerableExtensions
    {

        public static SeparatedSyntaxList<TSyntax> ToSeparatedSyntaxList<TSyntax>(this IEnumerable<TSyntax> source) where TSyntax : SyntaxNode
        {
            return new SeparatedSyntaxList<TSyntax>().AddRange(source);
        }

        public static SyntaxList<TSyntax> ToSyntaxList<TSyntax>(this IEnumerable<TSyntax> source) where TSyntax : SyntaxNode
        {
            return new SyntaxList<TSyntax>().AddRange(source);
        }

        public static SeparatedSyntaxList<TSyntax> ToSeparatedSyntaxList<TSyntax>(this TSyntax source) where TSyntax : SyntaxNode
        {
            return new SeparatedSyntaxList<TSyntax>().Add(source);
        }
    }
}