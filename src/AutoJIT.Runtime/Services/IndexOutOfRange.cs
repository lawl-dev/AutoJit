using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class IndexOutOfRange : AutoJITExceptionBase
    {
        public IndexOutOfRange( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}