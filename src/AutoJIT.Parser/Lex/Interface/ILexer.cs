using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.Lex.Interface
{
    public interface ILexer
    {
        TokenCollection Lex( string autoitScript );
    }
}
