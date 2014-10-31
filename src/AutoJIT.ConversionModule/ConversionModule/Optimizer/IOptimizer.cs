using Microsoft.CodeAnalysis;

namespace AutoJIT.CSharpConverter.ConversionModule.Optimizer
{
	public interface IOptimizer
	{
		SyntaxNode Optimize( SyntaxNode root );
	}
}
