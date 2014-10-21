using System;

namespace AutoJITRuntime.Exceptions
{
    internal class UnknowTypeNameException : Exception
    {
        public UnknowTypeNameException( string typeName ) {
            TypeName = typeName;
        }
        
        public string TypeName { get; set; }
    }
}
