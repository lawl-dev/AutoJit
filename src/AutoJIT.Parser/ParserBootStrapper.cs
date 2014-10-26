using AutoJIT.Parser.AST.Parser;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;
using AutoJIT.Parser.AST.Statements.Factory;
using AutoJIT.Parser.Lex;
using AutoJIT.Parser.Lex.Interface;
using AutoJIT.Parser.Optimizer;
using Lawl.Architekture;

namespace AutoJIT.Parser
{
    public class ParserBootStrapper : ComponentContainerBase
    {
        protected override void Bind() {
            Bind<IPragmaParser, PragmaParser>();
            Bind<ILexer, Lexer>();
            Bind<IStatementParser, StatementParser>();
            Bind<IExpressionParser, ExpressionParser>();
            Bind<IOptimizer, Optimizer.Optimizer>();
            Bind<ITokenFactory, TokenFactory>();
            Bind<IOperatorPrecedenceService, OperatorPrecedenceService>();
            Bind<IScriptParser, ScriptParser>();
            Bind<IAutoitStatementFactory, AutoitStatementFactory>();

            RegisterStatementParserStrategys();
        }

        private void RegisterStatementParserStrategys() {
            Bind<IStatementParserStrategyResolver, StatementParserStrategyResolver>();
            Bind<IStatementParserStrategy<AssignStatement>, AssignStatementParserStrategy>();
            Bind<IStatementParserStrategy<ContinueloopStatement>, ContinueLoopStatementStrategy>();
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
        }
    }
}
