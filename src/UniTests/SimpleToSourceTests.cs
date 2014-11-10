using System.Linq;
using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UniTests
{
    public class SimpleToSourceTests
    {
        private IExpressionParser _expressionParser;
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
            var token = _lexer.Lex( script ).Single();
            Assert.AreEqual(script, token.ToString());
        }

        [Test]
        public void TestKeywordToSource()
        {
            const string script = "Until";
            var token = _lexer.Lex(script).Single();
            Assert.AreEqual(script, token.ToString());
        }
    }
}