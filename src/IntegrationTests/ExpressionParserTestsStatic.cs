using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AutoJIT;
using AutoJIT.Compiler;
using AutoJIT.Parser.AST.Expressions;
using AutoJIT.Parser.AST.Expressions.Interface;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UnitTests
{
    public class ExpressionParserTestsStatic
    {
        private readonly ILexer _lexer;
        private readonly IExpressionParser _expressionParser;

        public ExpressionParserTestsStatic() {
            var componentContainer = new CompilerBootStrapper();
            _lexer = componentContainer.GetInstance<ILexer>();
            _expressionParser = componentContainer.GetInstance<IExpressionParser>();
        }

        [TestCase( "[-123 * 3 + 1, GetInt(1, GetInt(1, 2)), 5]" )]
        public void Test_ExpressionTree_ArrayInit( string arrInitExpress ) {
            var tokens = _lexer.Lex( arrInitExpress );

            IExpressionNode node = null;
            Assert.DoesNotThrow(
                () => { node = _expressionParser.ParseBlock( tokens, true ); } );

            var arrayInitExpression = (ArrayInitExpression) node;
            var childs = arrayInitExpression.ToAssign.ToList();
            for ( int i = 0; i < childs.Count(); i++ ) {
                Assert.IsTrue( childs[i].GetType() == GetExpectedType( i ) );
            }
        }

        public static double RoundUp( double input, int places ) {
            double multiplier = Math.Pow( 10, Convert.ToDouble( places ) );
            return Math.Ceiling( input * multiplier ) / multiplier;
        }

        public Type GetExpectedType( int i ) {
            switch (i) {
                case 0:
                    return typeof (BinaryExpression);
                case 2:
                    return typeof (NumericLiteralExpression);
                case 1:
                    return typeof (UserfunctionCallExpression);
            }
            throw new NotImplementedException();
        }
    }
}
