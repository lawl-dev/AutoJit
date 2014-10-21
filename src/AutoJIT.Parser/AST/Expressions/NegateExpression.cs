using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class NegateExpression : ExpressionBase
    {
        public NegateExpression( IExpressionNode expressionNode ) {
            ExpressionNode = expressionNode;
            NegateFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Negate( null ) );
            Initialize();
        }

        public IExpressionNode ExpressionNode { get; private set; }
        public string NegateFunctionName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return ExpressionNode.ToEnumerable(); }
        }

        public override string ToSource() {
            return string.Format( "-{0}", ExpressionNode.ToSource() );
        }

        public override object Clone() {
            return new NegateExpression( (IExpressionNode) ExpressionNode.Clone() );
        }
    }
}
