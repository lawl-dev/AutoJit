using System.Collections.Generic;

namespace AutoJITRuntime
{
    public static class RuntimeStore<T>
    {
        private static readonly Dictionary<string, T> Dictionary = new Dictionary<string, T>();

        public static bool Cached( string key ) {
            return Dictionary.ContainsKey( key );
        }

        public static void Cache( string key, T inst ) {
            Dictionary.Add( key, inst );
        }

        public static T Get( string key ) {
            return Dictionary[key];
        }
    }
}