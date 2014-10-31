namespace AutoJITRuntime.Exceptions
{
	public class VariablePassedToDllStructCreateWasNotAStringException : AutoJITExceptionBase
	{
		public VariablePassedToDllStructCreateWasNotAStringException( object error, object extended, object @return ) : base( error, extended, @return ) {}
	}
}
