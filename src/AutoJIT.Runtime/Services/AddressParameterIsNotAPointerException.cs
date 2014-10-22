using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class AddressParameterIsNotAPointerException : AutoJITExceptionBase {
        public AddressParameterIsNotAPointerException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}