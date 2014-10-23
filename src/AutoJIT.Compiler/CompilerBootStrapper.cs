using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.Parser;
using Lawl.Architekture;

namespace AutoJIT.Compiler
{
    public class CompilerBootStrapper : ComponentContainerBase
    {
        protected override void Bind() {
            Bind<ICompiler, Compiler>();
            Bind<IContinueCaseMsilFixingService, ContinueCaseMsilFixingService>();

            RegisterModule( new ParserBootStrapper() );
            RegisterModule( new ConversionBootStrapper() );
        }
    }
}
