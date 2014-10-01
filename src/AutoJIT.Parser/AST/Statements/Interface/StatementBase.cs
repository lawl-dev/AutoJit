using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Statements.Interface
{
    public abstract class StatementBase : SyntaxNodeBase, IStatementNode
    {
        public IEnumerable<TReturn> Accpet<TReturn>( IStatementVisitor<IEnumerable<TReturn>> visitor ) {
            return visitor.Visit( this );
        }
    }
}
