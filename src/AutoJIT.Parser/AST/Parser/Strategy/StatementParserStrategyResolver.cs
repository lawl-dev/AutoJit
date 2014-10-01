using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements.Interface;
using Lawl.Architekture;

namespace AutoJIT.Parser.AST.Parser.Strategy
{
    public sealed class StatementParserStrategyResolver : IStatementParserStrategyResolver
    {
        private readonly IDependencyContainer _dependencyContainer;

        public StatementParserStrategyResolver( IDependencyContainer dependencyContainer ) {
            _dependencyContainer = dependencyContainer;
        }

        public IStatementParserStrategy<TStatement> Resolve<TStatement>() where TStatement : IStatementNode {
            return _dependencyContainer.GetInstance<IStatementParserStrategy<TStatement>>();
        }
    }
}
