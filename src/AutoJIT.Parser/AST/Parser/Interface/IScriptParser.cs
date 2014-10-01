namespace AutoJIT.Parser.AST.Parser.Interface
{
    public interface IScriptParser
    {
        AutoitScriptRootNode ParseScript( string script, PragmaOptions pragmaOptions );
    }
}
