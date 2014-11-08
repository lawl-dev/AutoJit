using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class CallExpression : ExpressionBase
    {
        public CallExpression( string identifierName, IEnumerable<IExpressionNode> parameter ) {
            IdentifierName = identifierName;
            Parameter = parameter;
            Initialize();
        }

        public string IdentifierName { get; private set; }
        public IEnumerable<IExpressionNode> Parameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>( Parameter ); }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitCallExpression( this );
        }

        public override string ToSource() {
            return string.Format( "{0}({1})", IdentifierName, string.Join( ", ", Parameter.Select( x => x.ToSource() ) ) );
        }

        public override object Clone() {
            return new CallExpression( (string) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>( Parameter ) );
        }

        public virtual CallExpression Update( IList<IExpressionNode> parameter, string identifierName ) {
            if ( EnumerableEquals( Parameter, parameter ) &&
                 IdentifierName == identifierName ) {
                return this;
            }
            return new CallExpression( (string) identifierName.Clone(), parameter.Select( x => (IExpressionNode) x.Clone() ) );
        }
    }
}
