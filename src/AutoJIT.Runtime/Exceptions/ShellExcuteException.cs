namespace AutoJITRuntime.Exceptions
{
    public class ShellExcuteException : AutoJITExceptionBase
    {
        public ShellExcuteException( object error, object extended, object @return ) : base( error, extended, @return ) {}
    }
}