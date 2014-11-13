using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class ArrayExpression : VariableExpression
    {
        public ArrayExpression( string identifierName, IEnumerable<IExpressionNode> accessParameter ) : base( identifierName ) {
            AccessParameter = accessParameter;
            Initialize();
        }

        public IEnumerable<IExpressionNode> AccessParameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return AccessParameter; }
        }

        public override string ToSource() {
            var accessParameter = AccessParameter.Select( x => string.Format( "[{0}]", x.ToSource() ) ).ToList();
            return string.Format( "${0}{1}", IdentifierName, accessParameter.Single() );
        }

        public override object Clone() {
            return new ArrayExpression( (string) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( AccessParameter ) );
        }

        public ArrayExpression Update( string identifierName, IEnumerable<IExpressionNode> accessParameter ) {
            if ( identifierName == IdentifierName &&
                 EnumerableEquals(accessParameter, AccessParameter) ) {
                return this;
            }
            return new ArrayExpression( identifierName, accessParameter.Select( x=>(IExpressionNode)x.Clone() ) );
        }
    }
}
