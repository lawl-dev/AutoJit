using System.Collections.Generic;

namespace AutoJIT.Parser.Extensions
{
    internal static class CharExtensions
    {
        public static string Join( this IEnumerable<char> src ) {
            return string.Join( "", src );
        }
    }
}
