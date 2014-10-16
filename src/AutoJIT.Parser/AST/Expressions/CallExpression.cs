using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public class CallExpression : ExpressionBase
    {
        public string IdentifierName { get; private set; }
        public IEnumerable<IExpressionNode> Parameter { get; private set; }

        public CallExpression( string identifierName, IEnumerable<IExpressionNode> parameter ) {
            IdentifierName = identifierName;
            Parameter = parameter;
            Initialize();
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>( Parameter ); }
        }

        public override string ToSource() {
            return string.Format( "{0}({1})", IdentifierName, string.Join( ", ", Parameter ) );
        }

        public override object Clone() {
            return new CallExpression( (string) IdentifierName.Clone(), CloneEnumerableAs<IExpressionNode>(Parameter));
        }
    }
}
