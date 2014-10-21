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
            string _scriptTemplate = string.Format( "Func ExpressionReturner(){0}Return {{0}}{0}Endfunc{0}", Environment.NewLine );

            const string script = "DllCall(\"kernel32.dll\", \"bool\", \"Beep\", \"dword\", 3333, \"dword\", 4000)";
            string format = string.Format( _scriptTemplate, script );
            byte[] compile = _compiler.Compile( format, OutputKind.WindowsApplication, false );

            File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", compile );

            Assembly assembly = Assembly.Load( compile );
            Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            MethodInfo method = type.GetMethod( "f_ExpressionReturner" );
            object instance = type.CreateInstanceWithDefaultParameters();
            var result = method.Invoke( instance, null ) as Variant;
        }
    }
}
