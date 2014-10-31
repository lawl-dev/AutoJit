namespace AutoJIT.Parser.Extensions
{
	internal static class IntExtensions
	{
		public static int ToNullIfLessNull( this int value ) {
			if( value < 0 ) {
				return 0;
			}
			return value;
		}
	}
}
