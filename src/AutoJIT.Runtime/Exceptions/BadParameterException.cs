namespace AutoJITRuntime.Exceptions
{
    public class BadParameterException : AutoJITExceptionBase
    {
        public BadParameterException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}
