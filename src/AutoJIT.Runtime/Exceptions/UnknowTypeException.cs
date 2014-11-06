namespace AutoJITRuntime.Exceptions
{
    internal class UnknowTypeException : AutoJITExceptionBase
    {
        public UnknowTypeException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}
