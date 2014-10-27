namespace AutoJITRuntime.Exceptions
{
    public class BadReturnTypeException : AutoJITExceptionBase
    {
        public BadReturnTypeException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}
