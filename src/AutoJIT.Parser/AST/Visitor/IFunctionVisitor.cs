namespace AutoJIT.Parser.AST.Visitor
{
    public interface IFunctionVisitor<out T> : IFunctionSyntaxVisitor<FunctionNode, T> {}
}
