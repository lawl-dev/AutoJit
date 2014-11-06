namespace AutoJITRuntime.Exceptions
{
    internal class BadNumberOfParameterException : AutoJITExceptionBase
    {
        public BadNumberOfParameterException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}
