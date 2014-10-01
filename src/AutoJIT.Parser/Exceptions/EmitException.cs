using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AutoJIT.Parser.Exceptions
{
    public class EmitException : Exception
    {
        public IEnumerable<Diagnostic> Messages { get; set; }

        public EmitException( IEnumerable<Diagnostic> messages ) {
            Messages = messages;
        }

        public override string ToString() {
            return string.Join( Environment.NewLine, Messages );
        }
    }
}
