using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.CSharpConverter.ConversionModule.Optimizer;
using AutoJIT.Infrastructure;
using AutoJIT.Parser;
using AutoJIT.Parser.Service;

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
