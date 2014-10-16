using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public abstract class BinaryExpressionBase : ExpressionBase
    {
        public IExpressionNode Left { get; private set; }
        public IExpressionNode Right { get; private set; }
        public Token Operator { get; private set; }

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
