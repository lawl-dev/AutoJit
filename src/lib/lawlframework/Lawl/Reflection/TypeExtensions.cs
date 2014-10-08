using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawl.Reflection
{
    public static class TypeExtensions
    {
        public static T CreateInstance<T>( this Type src, object[] parameter)
        {
            var parameterTypes = parameter.Select( x=>x.GetType() ).ToArray();
            var constructorInfo = src.GetConstructor(parameterTypes);
            if (constructorInfo != null)
                return (T)constructorInfo.Invoke( parameter );
            return default(T);
        }

        public static T CreateInstanceWithDefaultParameters<T>(this Type src)
        {
            Type[] parameter = src.GetConstructors().First().GetParameters().Select(p => p.ParameterType).ToArray();
            return src.CreateInstance<T>(parameter);
        }
        
        public static object CreateInstanceWithDefaultParameters(this Type src)
        {
            Type[] parameter = src.GetConstructors().First().GetParameters().Select(p => p.ParameterType).ToArray();
            return CreateInstance<object>(src, parameter);
        }

        public static T CreateInstance<T>( this Type src, Type[] parameter)
        {
            var constructorInfo = src.GetConstructor(parameter);
            if ( constructorInfo != null ) {
                var invokeAttr = parameter.Select(x=>x.GetDefaultValue()).ToArray();
                return (T)constructorInfo.Invoke( invokeAttr );
            }
            return default(T);
        }

        public static bool TryCreateInstance<T>(this Type src, out T instance, object[] parameters)
        {
            try
            {
                var tmp = CreateInstance(src, parameters);
                if (tmp != null)
                {
                    instance = (T) tmp;
                    return true;
                }
                instance = default(T);
                return false;
            }
            catch
            {
                instance = default (T);
                return false;
            }
        }

        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }
            return null;
        }

        private static object CreateInstance(Type src, params object[] parameter)
        {
            Type[] types = parameter.Select(x => x.GetType()).ToArray();
            var constructorInfo = src.GetConstructor(types);
            return constructorInfo != null ? constructorInfo.Invoke(parameter) : null;
        }

        public static T CreateInstance<T>(this Type src)
        {
            return (T) CreateInstance(src);
        }
    }
}
