using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Statements
{
    public sealed class FunctionCallStatement : StatementBase
    {
        public IExpressionNode FunctionCallExpression { get; private set; }

        public FunctionCallStatement( IExpressionNode functionCallExpression ) {
            FunctionCallExpression = functionCallExpression;
            Initialize();
        }

        public override string ToSource() {
            return FunctionCallExpression.ToSource();
        }

        public override object Clone() {
            return new FunctionCallStatement( (IExpressionNode) FunctionCallExpression.Clone() );
        }

        public override IEnumerable<ISyntaxNode> Children {
            get { return new List<ISyntaxNode>() { FunctionCallExpression }; }
        }
    }
}
