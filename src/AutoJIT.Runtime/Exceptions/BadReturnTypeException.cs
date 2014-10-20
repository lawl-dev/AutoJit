using System;

namespace AutoJITRuntime.Exceptions
{
    public class BadReturnTypeException : Exception
    {
        public string ReturnType { get; private set; }

        public BadReturnTypeException( string returnType ) {
            ReturnType = returnType;
        }

        public override string ToString() {
            return string.Format( "Type: {0}", ReturnType );
        }
    }
}