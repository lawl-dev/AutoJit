using System.Collections.Generic;

namespace AutoJIT.Parser.AST
{
	public sealed class PragmaOptions
	{
		public PragmaOptions() {
			Includes = new List<string>();
			CompilerPragmas = new Dictionary<string, string>();
		}

		public List<string> Includes { get; set; }
		public bool NoTrayIcon { get; set; }
		public string OnAutoItStartRegister { get; set; }
		public Dictionary<string, string> CompilerPragmas { get; set; }
		public bool RequireAdmin { get; set; }
		public bool IncludeOnce { get; set; }
	}
}
