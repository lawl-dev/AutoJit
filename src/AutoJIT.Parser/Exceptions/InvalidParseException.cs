using System;

namespace AutoJIT.Parser.Exceptions
{
	public class InvalidParseException : Exception
	{
		public InvalidParseException( int line, int pos, string message ) : base( message ) {
			Line = line;
			Pos = pos;
		}

		public int Line { get; private set; }
		public int Pos { get; private set; }
	}
}
