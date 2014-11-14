using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class CallExpression : ExpressionBase
    {
        public CallExpression( TokenNode identifierName, IEnumerable<IExpressionNode> parameter ) {
            IdentifierName = identifierName;
            Parameter = parameter;
        }

        public TokenNode IdentifierName { get; private set; }
        public IEnumerable<IExpressionNode> Parameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>( Parameter );
                nodes.Add( IdentifierName );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitCallExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}({1})", IdentifierName.Token.Value.StringValue, string.Join( ", ", Parameter.Select( x => x.ToSource() ) ) );
        }

        public override object Clone() {
            var expression = new CallExpression( (TokenNode) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( Parameter ) );
            expression.Initialize();
            return expression;
        }

        public virtual CallExpression Update( IList<IExpressionNode> parameter, TokenNode identifierName ) {
            if ( EnumerableEquals( Parameter, parameter ) &&
                 IdentifierName == identifierName ) {
                return this;
            }
            var expression = new CallExpression( (TokenNode) identifierName.Clone(), parameter.Select( x => (IExpressionNode) x.Clone() ) );
            expression.Initialize();
            return expression;
        }
    }
}
