namespace AutoJIT.Parser.AST.Visitor
{
	public interface IExpressionSyntaxVisitor<in TIn, out TOut>
	{
		TOut Visit( TIn @in );
	}
}
