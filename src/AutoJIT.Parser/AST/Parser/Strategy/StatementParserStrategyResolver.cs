using AutoJIT.Infrastructure;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements.Interface;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class StatementParserStrategyResolver : IStatementParserStrategyResolver
    {
        private readonly IInjectionService _injectionService;

        public StatementParserStrategyResolver( IInjectionService injectionService ) {
            _injectionService = injectionService;
        }

        public IStatementParserStrategy<TStatement> Resolve<TStatement>() where TStatement : IStatementNode {
            return _injectionService.Inject<IStatementParserStrategy<TStatement>>();
        }
    }
}
