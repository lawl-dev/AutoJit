using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class UnableToUseTheDllFileException : AutoJITExceptionBase
    {
        public UnableToUseTheDllFileException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}