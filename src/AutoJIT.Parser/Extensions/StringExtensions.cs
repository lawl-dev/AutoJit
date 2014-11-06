using System.Linq;

namespace AutoJIT.Parser.Extensions
{
    internal static class StringExtensions
    {
        public static string ReplaceAt( this string value, int index, char newchar ) {
            return value.Length <= index
                ? value
                : string.Concat(
                    value.Select(
                        ( c, i ) => i == index
                            ? newchar
                            : c ) );
        }
    }
}
