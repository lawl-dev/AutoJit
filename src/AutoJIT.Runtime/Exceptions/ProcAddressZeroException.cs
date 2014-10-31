namespace AutoJITRuntime.Exceptions
{
	internal class ProcAddressZeroException : AutoJITExceptionBase
	{
		public ProcAddressZeroException( object error, object extended, object @return ) : base( error, extended, @return ) {}
	}
}
