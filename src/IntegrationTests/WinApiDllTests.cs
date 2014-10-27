using AutoJIT.Compiler;
using NUnit.Framework;

namespace UnitTests
{
    public class WinApiDllTests
    {
        private readonly ICompiler _compiler;

        public WinApiDllTests() {
            _compiler = new CompilerBootStrapper().GetInstance<ICompiler>();
        }

        [Test]
        public void Foo() {}
    }
}
