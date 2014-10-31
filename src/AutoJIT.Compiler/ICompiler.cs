using Microsoft.CodeAnalysis;

namespace AutoJIT.Compiler
{
	public interface ICompiler
	{
		byte[] Compile( string script, OutputKind outputKind, bool warningAsError, bool optimize = false, string assemblyName = "AutoJITAssembly" );
	}
}
