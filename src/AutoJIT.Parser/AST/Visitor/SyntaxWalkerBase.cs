namespace AutoJIT.Parser.AST.Visitor
{
    public abstract class SyntaxWalkerBase : SyntaxVisitorBase
    {
        public override void Visit( ISyntaxNode node ) {
            foreach (var child in node.Children) {
                Visit( child );
            }
        }
    }
}