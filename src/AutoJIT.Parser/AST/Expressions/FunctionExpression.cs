using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class FunctionExpression : ExpressionBase
    {
        public FunctionExpression( TokenNode identifierName ) {
            IdentifierName = identifierName;
        }

        public TokenNode IdentifierName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Enumerable.Empty<ISyntaxNode>(); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitFunctionExpression( this );
        }

        public override string ToSource() {
            return IdentifierName.ToSource();
        }

        public override object Clone() {
            return new FunctionExpression( IdentifierName );
        }

        public FunctionExpression Update( TokenNode identifierName ) {
            if ( IdentifierName == identifierName ) {
                return this;
            }
            return new FunctionExpression( (TokenNode) identifierName.Clone() );
        }
    }
}
