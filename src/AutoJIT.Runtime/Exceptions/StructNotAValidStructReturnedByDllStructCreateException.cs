namespace AutoJITRuntime.Exceptions
{
    public class StructNotAValidStructReturnedByDllStructCreateException : AutoJITExceptionBase
    {
        public StructNotAValidStructReturnedByDllStructCreateException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}
