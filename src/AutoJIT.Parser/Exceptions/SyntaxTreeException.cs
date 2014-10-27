using System;

namespace AutoJIT.Parser.Exceptions
{
    public class SyntaxTreeException : Exception
    {
        public SyntaxTreeException( string message, int col, int line ) : base( message ) {
            Col = col;
            Line = line;
        }

        public int Col {
            get;
            set;
        }
        public int Line {
            get;
            set;
        }
    }
}
