using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class BinaryExpressionBase : ExpressionBase
    {
        public readonly IExpressionNode Left;
        public readonly IExpressionNode Right;
        public readonly Token Operator;

        public abstract bool NeedsCompilerFunctionCall { get; }

        protected BinaryExpressionBase( IExpressionNode left, IExpressionNode right, Token @operator ) {
            Left = left;
            Right = right;
            Operator = @operator;
            Initialize();
        }

        public override string ToSource() {
            return string.Format( "{0} {1} {2}", Left.ToSource(), Operator, Right.ToSource() );
        }

        public abstract string GetCompilerFunctionName( TokenType @operatorType );
    }
}
