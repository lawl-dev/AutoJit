using System;
using System.Linq;

namespace AutoJIT.Parser.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetNonGenericName( this Type t ) {
            return t.Name.Remove( t.Name.IndexOf( '`' ) );
        }

        public static T CreateInstance<T>(this Type src, object[] parameter)
        {
            var parameterTypes = parameter.Select(x => x.GetType()).ToArray();
            var constructorInfo = src.GetConstructor(parameterTypes);
            if (constructorInfo != null)
                return (T)constructorInfo.Invoke(parameter);
            return default(T);
        }
    }
}
