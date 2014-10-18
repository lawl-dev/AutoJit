using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoJIT.Parser.Extensions
{
    internal static class CharExtensions
    {
        public static string Join( this IEnumerable<char> src ) {
            return string.Join( "", src );
        }
    }
}
