using AutoJIT.Parser.AST;

namespace AutoJIT.Parser.Lex.Interface
{
    public interface IPragmaParser
    {
        string IncludeDependenciesAndResolvePragmas( string autoitScript, PragmaOptions pragmaOptions );
    }
}
