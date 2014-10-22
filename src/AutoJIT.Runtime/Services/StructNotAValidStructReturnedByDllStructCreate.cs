using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class StructNotAValidStructReturnedByDllStructCreate : AutoJITExceptionBase
    {
        public StructNotAValidStructReturnedByDllStructCreate( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}