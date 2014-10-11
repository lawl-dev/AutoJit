using System;

namespace AutoJIT.CompilerApplication
{
    class CompileOptions
    {
        public Uri InFile { get; set; }
        public Uri OutFile { get; set; }
        public Uri Icon { get; set; }
        public bool IsConsole { get; set; }
        public bool IsForms { get; set; }
        public bool Optimize { get; set; }
    }
}