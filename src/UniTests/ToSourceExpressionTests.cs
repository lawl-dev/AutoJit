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


        [TestCase("1 == 2 ? False : True")]
        [TestCase("1 == 2 ? 2 == 3 ? False : True : 3 == 4 ? Call() : False")]
        public void Test_TernaryToSource(string ternary) {
            var token = _lexer.Lex(ternary);
            var node = _expressionParser.ParseBlock(token, true);
            Assert.AreEqual(ternary, node.ToSource());
        }
    }
}
