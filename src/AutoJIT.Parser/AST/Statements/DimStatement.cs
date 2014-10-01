using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class DimStatement : StatementBase
    {
        public readonly VariableExpression VariableExpression;
        public readonly IExpressionNode InitExpression;

        public DimStatement( VariableExpression variableExpression, IExpressionNode initExpression ) {
            VariableExpression = variableExpression;
            InitExpression = initExpression;
            Initialize();
        }

        public override string ToSource() {
            var toReturn = string.Format( "Dim {0}", VariableExpression.ToSource() );
            if ( InitExpression != null ) {
                toReturn += string.Format( " = {0}", InitExpression.ToSource() );
            }
            return toReturn;
        }

        public override object Clone() {
            return new DimStatement( (VariableExpression) VariableExpression.Clone(), (IExpressionNode) InitExpression.Clone() );
        }

        public override IEnumerable<ISyntaxNode> Children
        {
            get { return new List<ISyntaxNode>() { VariableExpression, InitExpression }; }
        }
    }
}
