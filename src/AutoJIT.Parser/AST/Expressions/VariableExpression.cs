using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class VariableExpression : ExpressionBase
    {
        public VariableExpression( TokenNode identifierName ) {
            IdentifierName = identifierName;
        }

        public TokenNode IdentifierName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitVariableExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}", IdentifierName.ToSource() );
        }

        public override object Clone() {
            return new VariableExpression( (TokenNode) IdentifierName.Clone() );
        }

        public VariableExpression Update( TokenNode identifierName ) {
            if ( IdentifierName == identifierName ) {
                return this;
            }
            return new VariableExpression( (TokenNode) identifierName.Clone() );
        }
    }
}
