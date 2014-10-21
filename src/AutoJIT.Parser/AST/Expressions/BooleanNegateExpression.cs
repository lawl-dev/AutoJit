using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Extensions;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class BooleanNegateExpression : ExpressionBase
    {
        public BooleanNegateExpression( IExpressionNode left, Token @operator ) {
            Left = left;
            Operator = @operator;
            NOTCompilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.NOT( null ) );
            Initialize();
        }

        public IExpressionNode Left { get; private set; }
        public Token Operator { get; private set; }
        public string NOTCompilerFunctionName { get; private set; }

        public override IEnumerable<ISyntaxNode> Children {
            get { return Left.ToEnumerable(); }
        }

        public override string ToSource() {
            return string.Format( "NOT {0}", Left.ToSource() );
        }

        public override object Clone() {
            return new BooleanNegateExpression( (IExpressionNode) Left.Clone(), Operator );
        }
    }
}
