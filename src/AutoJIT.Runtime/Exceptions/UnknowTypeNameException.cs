using System;

namespace AutoJITRuntime.Exceptions
{
    internal class UnknowTypeNameException : Exception
    {
        public UnknowTypeNameException( string typeName ) {
            TypeName = typeName;
        }

        public string TypeName { get; private set; }

        public override string Message {
            get { return ToString(); }
        }

        public override string ToString() {
            return string.Format( "TypeName: {0}", TypeName );
        }
    }
}
