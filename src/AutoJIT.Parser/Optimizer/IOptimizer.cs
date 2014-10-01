using Microsoft.CodeAnalysis;

namespace AutoJIT.Parser.Optimizer
{
    public interface IOptimizer
    {
        SyntaxNode Optimize( SyntaxNode root );
    }
}
