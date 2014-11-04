namespace AutoJIT.Parser.AST.Parser.Interface
{
	public interface IScriptParser
	{
		AutoitScriptRoot ParseScript( string script, PragmaOptions pragmaOptions );
	}
}
