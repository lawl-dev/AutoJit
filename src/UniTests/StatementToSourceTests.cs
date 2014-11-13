using System.IO;
using System.Linq;
using System.Text;
using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using NUnit.Framework;

namespace UniTests
{
    public class StatementToSourceTests
    {
        private IStatementParser _statementParser;
        private ILexer _lexer;

        public class TestContainer : ComponentContainerBase
        {
            protected override void Bind()
            {
                Bind<IExpressionParser, ExpressionParser>();
                Bind<IOperatorPrecedenceService, OperatorPrecedenceService>();
                Bind<ITokenFactory, TokenFactory>();
                Bind<ILexer, Lexer>();
                Bind<IStatementParser, StatementParser>();
                Bind<IStatementParserStrategyResolver, StatementParserStrategyResolver>();
                Bind<IStatementParserStrategy<AssignStatement>, AssignStatementParserStrategy>();
                Bind<IStatementParserStrategy<ContinueLoopStatement>, ContinueLoopStatementStrategy>();
                Bind<IStatementParserStrategy<DimStatement>, DimStatementParserStrategy>();
                Bind<IStatementParserStrategy<DoUntilStatement>, DoUntilStatementParserStrategy>();
                Bind<IStatementParserStrategy<ExitloopStatement>, ExitLoopStatementParserStrategy>();
                Bind<IStatementParserStrategy<ExitStatement>, ExitStatementParserStrategy>();
                Bind<IStatementParserStrategy<ForInStatement>, ForInStatementParserStrategy>();
                Bind<IStatementParserStrategy<ForToNextStatement>, ForToNextStatementParserStrategy>();
                Bind<IStatementParserStrategy<FunctionCallStatement>, FunctionCallStatementParserStrategy>();
                Bind<IStatementParserStrategy<GlobalDeclarationStatement>, GlobalStatementParserStrategy>();
                Bind<IStatementParserStrategy<GlobalEnumDeclarationStatement>, GlobalEnumParserStrategy>();
                Bind<IStatementParserStrategy<LocalEnumDeclarationStatement>, LocalEnumParserStrategy>();
                Bind<IStatementParserStrategy<IfElseStatement>, IfStatementParserStrategy>();
                Bind<IStatementParserStrategy<LocalDeclarationStatement>, LocalStatementParserStrategy>();
                Bind<IStatementParserStrategy<ReDimStatement>, RedimStatementParserStrategy>();
                Bind<IStatementParserStrategy<ReturnStatement>, ReturnStatementParserStrategy>();
                Bind<IStatementParserStrategy<SelectCaseStatement>, SelectStatementParserStrategy>();
                Bind<IStatementParserStrategy<SwitchCaseStatement>, SwitchCaseStatementParserStrategy>();
                Bind<IStatementParserStrategy<WhileStatement>, WhileStatementParserStrategy>();
                Bind<IStatementParserStrategy<ContinueCaseStatement>, ContinueCaseStatementStrategy>();
                Bind<IStatementParserStrategy<StaticDeclarationStatement>, StaticStatementParserStrategy>();
                Bind<IStatementParserStrategy<VariableFunctionCallStatement>, VariableFunctionCallStatementParserStrategy>();
                Bind<IAutoitStatementFactory, AutoitStatementFactory>();
            }
        }

        [SetUp]
        public void SetUp()
        {
            var testContainer = new StatementToSourceTests.TestContainer();

            _statementParser = testContainer.GetInstance<IStatementParser>();
            _lexer = testContainer.GetInstance<ILexer>();
        }


        [TestCase(@"If $sString > 0 Then
	MsgBox($MB_SYSTEMMODAL, '', 'Value is positive.')
ElseIf $sString < 0 Then
	MsgBox($MB_SYSTEMMODAL, '', 'Value is negative.')
Else
	If StringIsXDigit($sString) Then
		MsgBox($MB_SYSTEMMODAL, '', 'Value might be hexadecimal!')
	Else
		MsgBox($MB_SYSTEMMODAL, '', 'Value is a string.')
	EndIf
EndIf
")]
        public void TestIfToSource( string @if ) {
            var token = _lexer.Lex( @if );
            var tree = _statementParser.ParseBlock( token ).Single();
            var res = tree.ToSource();
            Assert.AreEqual( @if, res );
        }


        [TestCase(@"Switch @HOUR
	Case 6 To 11
		$sMsg = 'Good Morning'
	Case 12 To 17
		$sMsg = 'Good Afternoon'
	Case 18 To 21
		$sMsg = 'Good Evening'
	Case Else
		$sMsg = 'What are you still doing up?'
EndSwitch")]
        public void TestSwitchToSource(string @switch)
        {
            var token = _lexer.Lex(@switch);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(@switch, res);
        }


        [TestCase(@"Select
	Case $iValue = 1
		MsgBox($MB_SYSTEMMODAL, '', 'The first expression was True.')
	Case $sBlank = 'Test'
		MsgBox($MB_SYSTEMMODAL, '', 'The second expression was True')
	Case Else
		MsgBox($MB_SYSTEMMODAL, '', 'No preceding Case was True.')
EndSelect")]
        public void TestSelectToSource(string @select)
        {
            var token = _lexer.Lex(@select);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(@select, res);
        }



        [TestCase(@"Dim $vVariableThatIsGlobal = 'This is a variable that has ""Program Scope"" aka Global.'")]
        public void TestDimToSource(string dim)
        {
            var token = _lexer.Lex(dim);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(dim, res);
        }



        [TestCase(@"Do
	MsgBox($MB_SYSTEMMODAL, '', 'The value of $i is: ' & $i)
	$i = $i + 1
Until $i = 10")]
        public void TestDoUntilToSource(string doUntil)
        {
            var token = _lexer.Lex(doUntil);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(doUntil, res);
        }



        [TestCase(@"For $i = 5 To 1 Step -1
	MsgBox($MB_SYSTEMMODAL, '', 'Count down!' & @CRLF & $i)
Next")]
        public void TestForToStepNextToSource(string forToStepNext)
        {
            var token = _lexer.Lex(forToStepNext);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(forToStepNext, res);
        }




        [TestCase(@"For $vElement In $aArray
	$sString = $sString & $vElement & @CRLF
Next")]
        public void TestForInNextToSource(string forInNext)
        {
            var token = _lexer.Lex(forInNext);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(forInNext, res);
        }


        [TestCase(@"Local $vReturn = SomeFunc()")]
        public void TestLocalToSource(string local)
        {
            var token = _lexer.Lex(local);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(local, res);
        }


        [TestCase(@"ReDim $aArray[UBound($aArray) + 1]")]
        public void TestRedimToSource(string redim)
        {
            var token = _lexer.Lex(redim);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(redim, res);
        }


        [TestCase(@"While $i <= 10
	MsgBox($MB_SYSTEMMODAL, '', 'Value of $i is: ' & $i)
	$i = $i + 1
WEnd")]
        public void TestWhileWendToSource(string @while)
        {
            var token = _lexer.Lex(@while);
            var tree = _statementParser.ParseBlock(token).Single();
            var res = tree.ToSource();
            Assert.AreEqual(@while, res);
        }
    }
}