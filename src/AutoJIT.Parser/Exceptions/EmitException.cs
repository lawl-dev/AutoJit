using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoJIT.Parser.Exceptions
{
	public class EmitException : Exception
	{
		public EmitException( IEnumerable<Diagnostic> messages ) {
			Messages = messages;
		}

		public IEnumerable<Diagnostic> Messages { get; set; }

		public override string Message {
			get {
				return ToString();
			}
		}

		public override string ToString() {
			return string.Join( Environment.NewLine, Messages.Where( x => x.Severity == DiagnosticSeverity.Error ) );
		}
	}
}
