using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Collection;

namespace AutoJIT.Parser.AST.Parser.Interface
{
    public interface IExpressionParser
    {
        IExpressionNode ParseBlock( TokenCollection block, bool prepareExpression );
        T ParseSingle<T>( TokenQueue block ) where T : IExpressionNode;
    }
}
