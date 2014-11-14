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
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( IdentifierName );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitVariableExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}", IdentifierName.ToSource() );
        }

        public override object Clone() {
            var expression = new VariableExpression( (TokenNode) IdentifierName.Clone() );
            expression.Initialize();
            return expression;
        }

        public VariableExpression Update( TokenNode identifierName ) {
            if ( IdentifierName == identifierName ) {
                return this;
            }
            var expression = new VariableExpression( (TokenNode) identifierName.Clone() );
            expression.Initialize();
            return expression;
        }
    }
}
