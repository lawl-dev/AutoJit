using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class ArrayExpression : VariableExpression
    {
        public ArrayExpression( TokenNode identifierName, List<IExpressionNode> accessParameter ) : base( identifierName ) {
            AccessParameter = accessParameter;
        }

        public List<IExpressionNode> AccessParameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>(AccessParameter);
                nodes.Add( IdentifierName );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitArrayExpression( this );
        }

        public override string ToSource() {
            var accessParameter = AccessParameter.Select( x => string.Format( "[{0}]", x.ToSource() ) ).ToList();
            return string.Format( "{0}{1}", IdentifierName.ToSource(), string.Join( string.Empty, accessParameter) );
        }

        public override object Clone() {
            var toReturn = new ArrayExpression( (TokenNode) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( AccessParameter ).ToList() );
            toReturn.Initialize();
            return toReturn;
        }

        public ArrayExpression Update( TokenNode identifierName, List<IExpressionNode> accessParameter ) {
            if ( identifierName == IdentifierName &&
                 EnumerableEquals(accessParameter, AccessParameter) ) {
                return this;
            }
            var expression = new ArrayExpression( (TokenNode) identifierName.Clone(), accessParameter.Select( x=>(IExpressionNode)x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }
    }
}
