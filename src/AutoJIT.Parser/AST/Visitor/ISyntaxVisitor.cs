namespace AutoJIT.Parser.AST.Visitor
{
    public interface ISyntaxVisitor
    {
        void Visit( ISyntaxNode node );
    }

    public interface ISyntaxVisitor<out TResult>
    {
        TResult Visit( ISyntaxNode node );
    }
}
