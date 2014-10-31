using System.Collections.Generic;

namespace AutoJIT.Parser
{
	public static class Utils
	{
		public static IEnumerable<T> GetEnumerable<T>( params T[] ts ) {
			return ts;
		}
	}
}
