namespace AutoJIT.Parser.AST.Visitor
{
    public interface IFunctionSyntaxVisitor<in T, out T1>
    {
        T1 Visit( T @in );
    }
}
