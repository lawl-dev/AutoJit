using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Visitor
{
    public interface IStatementVisitor<out TReturn> : ISyntaxVisitor<IStatementNode, TReturn> {}
}
