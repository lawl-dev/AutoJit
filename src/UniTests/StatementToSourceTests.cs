using System.IO;
using System.Text;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UniTests
{
    public class StatementToSourceTests
    {
        private IExpressionParser _expressionParser;
        private ILexer _lexer;

        public class TestContainer : ComponentContainerBase
        {
            protected override void Bind()
            {
                Bind<IExpressionParser, ExpressionParser>();
                Bind<IOperatorPrecedenceService, OperatorPrecedenceService>();
                Bind<ITokenFactory, TokenFactory>();
                Bind<ILexer, Lexer>();
            }
        }

        [SetUp]
        public void SetUp()
        {
            var testContainer = new ToSourceExpressionTests.TestContainer();

            _expressionParser = testContainer.GetInstance<IExpressionParser>();
            _lexer = testContainer.GetInstance<ILexer>();
        }

        [TestCase("If 1 == 2 Then")]
        public void TestIfToSource( string @if ) {
            
        }
    }
}