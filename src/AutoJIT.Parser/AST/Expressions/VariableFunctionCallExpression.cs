using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class VariableFunctionCallExpression : ExpressionBase
    {
        public VariableFunctionCallExpression( VariableExpression variableExpression, IEnumerable<IExpressionNode> parameter ) {
            VariableExpression = variableExpression;
            Parameter = parameter;
        }

        public VariableExpression VariableExpression { get; private set; }
        public IEnumerable<IExpressionNode> Parameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( VariableExpression );
                nodes.AddRange( Parameter );
                return nodes;
            }
        }

        public override TResult Accept<TResult>( SyntaxVisitorBase<TResult> visitor ) {
            return visitor.VisitVariableFunctionCallExpression( this );
        }

        public override string ToSource() {
            string parameters = string.Join( ", ", Parameter.Select( x => x.ToSource() ) );
            return string.Format( "{0}({1})", VariableExpression.ToSource(), parameters );
        }

        public override object Clone() {
            return new VariableFunctionCallExpression( (VariableExpression) VariableExpression.Clone(), Parameter.Select( x => (IExpressionNode) x.Clone() ) );
        }

        public VariableFunctionCallExpression Update( VariableExpression variableExpression, IEnumerable<IExpressionNode> parameter ) {
            if ( VariableExpression == variableExpression &&
                 Parameter == parameter ) {
                return this;
            }
            return new VariableFunctionCallExpression( variableExpression, parameter );
        }
    }
}
