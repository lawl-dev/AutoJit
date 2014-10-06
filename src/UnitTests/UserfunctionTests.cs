using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoJIT.Compiler;
using AutoJITScript;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class UserfunctionTests
    {
        private readonly ICompiler _compiler;

        public UserfunctionTests()
        {
            var standardAutoJITContainer = new CompilerBootStrapper();
            _compiler = standardAutoJITContainer.GetInstance<ICompiler>();
        }


        [TestCase("_WinAPI_GetIconInfo.au3")]
        public void Test_compile_includes(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            Assert.DoesNotThrow(() =>
            {
                var assemblyBytes = _compiler.Compile(script, OutputKind.DynamicallyLinkedLibrary, false);
                var assembly = Assembly.Load(assemblyBytes);
                var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
                var method = type.GetMethod("f_Example");
                var instance = type.CreateInstanceWithDefaultParameters();
                var res = method.Invoke( instance, null );
            });

        }
        
        
        [TestCase("_WinAPI_CreateFile.au3")]
        public void Test_compile_includes3(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            Assert.DoesNotThrow(() =>
            {
                var assemblyBytes = _compiler.Compile(script, OutputKind.DynamicallyLinkedLibrary, false);
                File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.dll", assemblyBytes);

                var assembly = Assembly.Load(assemblyBytes);
                var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
                var method = type.GetMethod("f_Example");
                var instance = type.CreateInstanceWithDefaultParameters();
                var res = method.Invoke( instance, null );
            });

        }


        [TestCase("_WinAPI_EqualMemory.au3")]
        public void Test_compile_includes4(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            Assert.DoesNotThrow(() =>
            {
                var assemblyBytes = _compiler.Compile(script, OutputKind.DynamicallyLinkedLibrary, false);
                File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.dll", assemblyBytes);

                var assembly = Assembly.Load(assemblyBytes);
                var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
                var method = type.GetMethod("f_Example");
                var instance = type.CreateInstanceWithDefaultParameters();
                var res = method.Invoke( instance, null );
            });

        }


        [TestCase("Benchmark.au3")]
        public void Test_compile_Benchmark(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            Assert.DoesNotThrow(() =>
            {
                var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
                File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes);
                var process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                while ( !process.HasExited ) {
                    Thread.Sleep( 1000 );
                }
                Debug.Write(process.ExitCode);
            });

        }
        
        
        [TestCase("DES.au3")]
        public void Test_compile_DES(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            Assert.DoesNotThrow(() =>
            {
                var assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
                File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes);
                
                var process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                while ( !process.HasExited ) {
                    Thread.Sleep( 1000 );
                }
                Debug.Write(process.ExitCode);
            });

        }

        [Test]
        public void Foo() {
            Console.WriteLine(1111 << 1);
            Console.WriteLine(1111 >> -1);

            var autoJITScriptClass = new AutoJITScriptClass();

        }
    }
}