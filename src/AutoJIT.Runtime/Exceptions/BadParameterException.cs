using System;

namespace AutoJITRuntime.Exceptions
{
    public class BadParameterException : Exception
    {
        public string TypeName { get; private set; }
        public object Value { get; private set; }

        public BadParameterException( string typeName, object value) {
            TypeName = typeName;
            Value = value;
        }

        public override string ToString() {
            return string.Format( "Type: {0} Value: {1}", TypeName, Value );
        }
    }
}