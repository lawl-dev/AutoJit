using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJITRuntime;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
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
        public void Foo() {
        }
    }
}
