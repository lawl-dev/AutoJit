using System.Collections.Generic;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.Helper;
using AutoJIT.Parser.Lex;

namespace AutoJIT.Parser.AST.Expressions
{
    public sealed class BinaryExpression : BinaryExpressionBase
    {
        public BinaryExpression( IExpressionNode left, IExpressionNode right, Token @operator ) : base( left, right, @operator ) {}

        public override bool NeedsCompilerFunctionCall {
            get {
                return Operator.Type == TokenType.StringEqual || Operator.Type == TokenType.Pow
                       || Operator.Type == TokenType.AND || Operator.Type == TokenType.OR || Operator.Type == TokenType.Concat;
            }
        }

        public override string GetCompilerFunctionName( TokenType operatorType ) {
            string compilerFunctionName = null;
            switch (operatorType) {
                case TokenType.StringEqual:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.EqualString( null, null ) );
                    break;
                case TokenType.AND:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.AND( null, null ) );
                    break;
                case TokenType.OR:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.OR( null, null ) );
                    break;
                case TokenType.Concat:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Concat( null, null ) );
                    break;
                case TokenType.Pow:
                    compilerFunctionName = CompilerHelper.GetCompilerMemberName( x => x.Pow( null, null ) );
                    break;
            }
            return compilerFunctionName;
        }

        public override IEnumerable<ISyntaxNode> Children {
            get {
                var toReturn = new List<ISyntaxNode> { Left, Right };
                return toReturn;
            }
        }
        
        public override object Clone() {
            return new BinaryExpression( (IExpressionNode) Left.Clone(), (IExpressionNode) Right.Clone(), Operator );
        }
    }
}
