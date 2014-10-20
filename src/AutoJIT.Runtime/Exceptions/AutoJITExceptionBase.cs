using System;

namespace AutoJITRuntime.Exceptions
{
    public abstract class AutoJITExceptionBase : Exception
    {
        public object Error { get;private set; }
        public object Extended { get; private set; }
        public object Return { get; private set; }

        protected AutoJITExceptionBase(object error, object extended, object @return) {
            Error = error;
            Extended = extended;
            Return = @return;
        }
    }
}