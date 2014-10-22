using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class ElementOutOfRange : AutoJITExceptionBase
    {
        public ElementOutOfRange( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}