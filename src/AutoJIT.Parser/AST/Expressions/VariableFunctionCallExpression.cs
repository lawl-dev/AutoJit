using System.Collections.Generic;
using System.Linq;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Visitor;

namespace AutoJIT.Parser.AST.Expressions
{
    public class VariableFunctionCallExpression : ExpressionBase
    {
        public VariableFunctionCallExpression( VariableExpression variableExpression, List<IExpressionNode> parameter ) {
            VariableExpression = variableExpression;
            Parameter = parameter;
        }

        public VariableExpression VariableExpression { get; private set; }
        public List<IExpressionNode> Parameter { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var nodes = new List<ISyntaxNode>();
                nodes.Add( VariableExpression );
                nodes.AddRange( Parameter );
                return nodes;
            }
        }

        public override void Accept( ISyntaxVisitor visitor ) {
            visitor.VisitVariableFunctionCallExpression(this);
        }

        public override TResult Accept<TResult>( ISyntaxVisitor<TResult> visitor ) {
            return visitor.VisitVariableFunctionCallExpression( this );
        }

        public override string ToSource() {
            string parameters = string.Join( ", ", Parameter.Select( x => x.ToSource() ) );
            return string.Format( "{0}({1})", VariableExpression.ToSource(), parameters );
        }

        public override object Clone() {
            var expression = new VariableFunctionCallExpression( (VariableExpression) VariableExpression.Clone(), Parameter.Select( x => (IExpressionNode) x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }

        public VariableFunctionCallExpression Update( VariableExpression variableExpression, IEnumerable<IExpressionNode> parameter ) {
            if ( VariableExpression == variableExpression &&
                 EnumerableEquals(Parameter, parameter) ) {
                return this;
            }
            var expression = new VariableFunctionCallExpression( (VariableExpression) variableExpression.Clone(), parameter.Select( x=>(IExpressionNode)x.Clone() ).ToList() );
            expression.Initialize();
            return expression;
        }
    }
}
