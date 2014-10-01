using System;

namespace AutoJIT.Parser.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetNonGenericName( this Type t ) {
            return t.Name.Remove( t.Name.IndexOf( '`' ) );
        }
    }
}
