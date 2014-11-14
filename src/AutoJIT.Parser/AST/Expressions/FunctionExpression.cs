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
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( IdentifierName );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitFunctionExpression( this );
        }

        public override string ToSource() {
            return IdentifierName.ToSource();
        }

        public override object Clone() {
            var expression = new FunctionExpression( (TokenNode) IdentifierName.Clone() );
            expression.Initialize();
            return expression;
        }

        public virtual FunctionExpression Update( TokenNode identifierName ) {
            if ( IdentifierName == identifierName ) {
                return this;
            }
            var expression = new FunctionExpression( (TokenNode) identifierName.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
