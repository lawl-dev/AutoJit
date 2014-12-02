using AutoJIT.Parser.AST.Parser.Strategy;
using AutoJIT.Parser.AST.Parser.Strategy.Interface;
using AutoJIT.Parser.AST.Statements;

namespace AutoJIT.Parser
{
    public class OOPParserBootStrapper : StandardParserBootStrapper
    {
        protected override void RegisterStatementParserStrategys() {
            base.RegisterStatementParserStrategys();
            Bind<IStatementParserStrategy<PropertyDeclarationStatement>, PropertyDeclarationParserStrategy>();
        }
    }
}