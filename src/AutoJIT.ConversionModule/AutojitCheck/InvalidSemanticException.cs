using System;

namespace AutoJIT.CSharpConverter.AutojitCheck
{
	public class InvalidSemanticException : Exception
	{
		private readonly string _message;

		public InvalidSemanticException( string message ) {
			_message = message;
		}

		public override string ToString() {
			return _message;
		}
	}
}
