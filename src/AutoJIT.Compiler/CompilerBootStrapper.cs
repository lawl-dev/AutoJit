using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.CSharpConverter.ConversionModule.Optimizer;
using AutoJIT.Parser;
using Lawl.Architekture;

namespace AutoJIT.Compiler
{
    public class CompilerBootStrapper : ComponentContainerBase
    {
        protected override void Bind() {
            Bind<ICompiler, Compiler>();
            Bind<IContinueCaseMsilFixingService, ContinueCaseMsilFixingService>();
            Bind<IOptimizer, Optimizer>();

            RegisterModule( new ParserBootStrapper() );
            RegisterModule( new ConversionBootStrapper() );
        }
    }
}
