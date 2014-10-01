using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class ExpressionBase : SyntaxNodeBase, IExpressionNode
    {
        public TReturn Accpet<TReturn>( IExpressionVisitor<TReturn> visitor ) {
            return visitor.Visit( this );
        }
    }
}
