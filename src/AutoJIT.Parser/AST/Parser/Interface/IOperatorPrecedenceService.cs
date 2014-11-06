using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.AST.Parser.Interface
{
    public interface IOperatorPrecedenceService
    {
        TokenCollection PrepareOperatorPrecedence( TokenCollection expression );
    }
}
