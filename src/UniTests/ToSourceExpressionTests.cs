using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UniTests
{
    public class ToSourceExpressionTests
    {
        private IExpressionParser _expressionParser;
        private ILexer _lexer;

        public class TestContainer : ComponentContainerBase
        {
            protected override void Bind() {
                Bind<IExpressionParser, ExpressionParser>();
                Bind<IOperatorPrecedenceService, OperatorPrecedenceService>();
                Bind<ITokenFactory, TokenFactory>();
                Bind<ILexer, Lexer>();
            }
        }

        [SetUp]
        public void SetUp() {
            var testContainer = new TestContainer();

            _expressionParser = testContainer.GetInstance<IExpressionParser>();
            _lexer = testContainer.GetInstance<ILexer>();
        }

        [TestCase("1 + 2")]
        [TestCase("1 - 2")]
        [TestCase("1 * 2")]
        [TestCase("1 / 2")]
        public void Test_BinaryExpressionToSource(string @case) {
            var token = _lexer.Lex( @case );
            var node = _expressionParser.ParseBlock( token, true );
            Assert.AreEqual( @case, node.ToSource() );
        }

        [TestCase("$a + 2")]
        [TestCase("$b - 2")]
        [TestCase("$c * 2")]
        [TestCase("$d / 2")]
        [TestCase("1 + $a")]
        [TestCase("1 - $b")]
        [TestCase("1 * $c")]
        [TestCase("1 / $d")]
        public void Test_BinaryExpressionWithVariableToSource(string @case)
        {
            var token = _lexer.Lex(@case);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(@case, node.ToSource());
        }


        [TestCase("$a[0] + 2")]
        [TestCase("$b[0][10] - 2")]
        [TestCase("$c[0][10][10] * 2")]
        [TestCase("$d[111.23] / 2")]
        [TestCase("1 + $a[111.23][123]")]
        public void Test_BinaryExpressionWithArrayToSource(string @case)
        {
            var token = _lexer.Lex(@case);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(@case, node.ToSource());
        }


        [TestCase("1 == 2 ? False : True")]
        [TestCase("1 == 2 ? 2 == 3 ? False : True : 3 == 4 ? Call() : False")]
        public void Test_TernaryToSource(string ternary) {
            var token = _lexer.Lex(ternary);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(ternary, node.ToSource());
        }

        [TestCase("Dim $a[10] = [1, 2, 3, 4, 5]")]
        public void Test_ArrayInitExpression(string initExpression) {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("Not True")]
        public void Test_BooleanNegateExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }


        [TestCase("Call123(1, 2, 'sesf')")]
        public void Test_CallExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("Default")]
        public void Test_DefaultExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("False")]
        public void Test_FalseExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("True")]
        public void Test_TrueExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("Call")]
        public void Test_FunctionExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("@MIN")]
        public void Test_MacroExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }

        [TestCase("Null")]
        public void Test_NullExpression(string initExpression)
        {
            var token = _lexer.Lex(initExpression);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(initExpression, node.ToSource());
        }
    }
}
