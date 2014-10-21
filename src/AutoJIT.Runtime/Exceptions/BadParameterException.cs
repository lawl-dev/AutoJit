using System;

namespace AutoJITRuntime.Exceptions
{
    public class BadParameterException : Exception
    {
        public BadParameterException( string typeName, object value ) {
            TypeName = typeName;
            Value = value;
        }

        public string TypeName { get; private set; }
        public object Value { get; private set; }

        public override string ToString() {
            return string.Format( "Type: {0} Value: {1}", TypeName, Value );
        }
    }
}
