using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;

namespace AutoJIT.Parser.AST.Expressions
{
    public class VariableExpression : ExpressionBase
    {
        public VariableExpression( string identifierName ) {
            IdentifierName = identifierName;
        }

        public string IdentifierName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<IExpressionNode>(); }
        }

        public override string ToSource() {
            return string.Format( "${0}", IdentifierName );
        }

        public override object Clone() {
            return new VariableExpression( (string) IdentifierName.Clone() );
        }
    }
}
