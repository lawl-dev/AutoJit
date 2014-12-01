using AutoJIT.Contrib;
using AutoJIT.Parser.AST.Factory;
using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;

namespace AutoJIT.Parser
{
    public class StandardParserBootStrapper : ComponentContainerBase
    {
        protected override void Bind() {
            Bind<IPragmaParser, PragmaParser>();
            Bind<ILexer, Lexer>();
            Bind<IStatementParser, StatementParser>();
            Bind<IExpressionParser, ExpressionParser>();
            Bind<ITokenFactory, TokenFactory>();
            Bind<IOperatorPrecedenceService, OperatorPrecedenceService>();
            Bind<IScriptParser, ScriptParser>();
            Bind<IAutoitSyntaxFactory, AutoitSyntaxFactory>();

            RegisterStatementParserStrategys();
        }

        protected virtual void RegisterStatementParserStrategys() {
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
            Bind<IStatementParserStrategy<EmptyStatement>, EmptyStatementParserStrategy>();
        }
    }
}
