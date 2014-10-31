using System.Collections.Generic;

namespace AutoJIT.Parser.Extensions
{
	public static class GenericExtensions
	{
		public static IEnumerable<T> ToEnumerable<T>( this T t ) {
			return new[] {
				t
			};
		}
	}
}
