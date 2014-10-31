namespace AutoJIT.Parser.AST.Visitor
{
	public interface ISyntaxVisitor
	{
		void Visit( ISyntaxNode node );
	}

	public interface ISyntaxVisitor<in TIn, out TOut> : ISyntaxVisitor
	{
		TOut Visit( TIn node );
	}
}
