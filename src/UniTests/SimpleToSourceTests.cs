using System.Globalization;
using System.Linq;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UniTests
{
    public class SimpleToSourceTests
    {
        private ILexer _lexer;

        public class TestContainer : ComponentContainerBase
        {
            protected override void Bind() {
                Bind<ITokenFactory, TokenFactory>();
                Bind<ILexer, Lexer>();
            }
        }

        [SetUp]
        public void SetUp() {
            var testContainer = new TestContainer();

            _lexer = testContainer.GetInstance<ILexer>();
        }

        [Test]
        public void TestVariableToSource() {
            const string script = "$variable";
            var token = _lexer.Lex( script ).First();
            Assert.AreEqual(script, token.ToString());
        }

        [Test]
        public void TestKeywordToSource()
        {
            const string script = "Until";
            var token = _lexer.Lex(script).First();
            Assert.AreEqual(script, token.ToString());
        }

        [TestCase("0x01", 1)]
        [TestCase("2", 2)]
        public void TestIntToSource(string script, int expected)
        {
            var token = _lexer.Lex(script).First();
            Assert.AreEqual(expected.ToString(), token.ToString());
        }

        [TestCase("0.01", 0.01d)]
        [TestCase(".02", .02d)]
        [TestCase("12e3", 12e3)]
        public void TestDoubleToSource(string script, double expected)
        {
            var token = _lexer.Lex(script).First();
            Assert.AreEqual(expected.ToString( CultureInfo.InvariantCulture ), token.ToString());
        }

        [TestCase("\"Hallo Omi\"", "\'Hallo Omi\'")]
        public void TestStringToSource(string script, string expected)
        {
            var token = _lexer.Lex(script).First();
            Assert.AreEqual(expected.ToString(CultureInfo.InvariantCulture), token.ToString());
        }
    }
}