using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class ReturnStatement : StatementBase
    {
        public ReturnStatement( IExpressionNode returnExpression ) {
            ReturnExpression = returnExpression;
            Initialize();
        }

        public IExpressionNode ReturnExpression { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode> { ReturnExpression }; }
        }

        public override string ToSource() {
            return string.Format( "Return {0}", ReturnExpression.ToSource() );
        }

        public override object Clone() {
            return new ReturnStatement( CloneAs<IExpressionNode>( ReturnExpression ) );
        }
    }
}
