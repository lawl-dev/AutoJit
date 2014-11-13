using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class ArrayExpression : VariableExpression
    {
        public ArrayExpression( TokenNode identifierName, IEnumerable<IExpressionNode> accessParameter ) : base( identifierName ) {
            AccessParameter = accessParameter;
            Initialize();
        }

        public IEnumerable<IExpressionNode> AccessParameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return AccessParameter; }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitArrayExpression( this );
        }

        public override string ToSource() {
            var accessParameter = AccessParameter.Select( x => string.Format( "[{0}]", x.ToSource() ) ).ToList();
            return string.Format( "${0}{1}", IdentifierName.ToSource(), string.Join( string.Empty, accessParameter) );
        }

        public override object Clone() {
            return new ArrayExpression( (TokenNode) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( AccessParameter ) );
        }

        public ArrayExpression Update( TokenNode identifierName, IEnumerable<IExpressionNode> accessParameter ) {
            if ( identifierName == IdentifierName &&
                 EnumerableEquals(accessParameter, AccessParameter) ) {
                return this;
            }
            return new ArrayExpression( identifierName, accessParameter.Select( x=>(IExpressionNode)x.Clone() ) );
        }
    }
}
