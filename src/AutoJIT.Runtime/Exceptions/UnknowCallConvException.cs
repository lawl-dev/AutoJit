using System;

namespace AutoJITRuntime.Exceptions
{
    internal class UnknowCallConvException : Exception
    {
        public UnknowCallConvException( string callConv ) {
            CallConv = callConv;
        }

        public string CallConv { get; private set; }

        public override string Message {
            get {
                return ToString();
            }
        }

        public override string ToString() {
            return string.Format( "Conv: {0}", CallConv );
        }
    }
}
