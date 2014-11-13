using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJIT.Contrib;
using AutoJITRuntime;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class MarshalTests
    {
        [SetUp]
        public void Setup() {
            var standardAutoJITContainer = new CompilerBootStrapper();
            var compiler = standardAutoJITContainer.GetInstance<ICompiler>();
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, "Unmanaged.au3" );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = compiler.Compile( script, OutputKind.ConsoleApplication, false );

                    Assembly assembly = Assembly.Load( assemblyBytes );
                    Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    _compiledInstance = type.GetConstructors()[0].Invoke( Constants.Array<object>.Empty );
                } );
        }

        private object _compiledInstance;

        [TestCase( "FA" )]
        [TestCase( "FB" )]
        [TestCase( "FC" )]
        [TestCase( "FD" )]
        [TestCase( "FE" )]
        [TestCase( "FF" )]
        [TestCase( "FG" )]
        [TestCase( "FH" )]
        [TestCase( "FI" )]
        [TestCase( "FJ" )]
        [TestCase( "FK" )]
        [TestCase( "FL" )]
        [TestCase( "FRA" )]
        [TestCase( "FRB" )]
        [TestCase( "FRC" )]
        [TestCase( "FRD" )]
        [TestCase( "FRE" )]
        [TestCase( "FRF" )]
        [TestCase( "FRG" )]
        [TestCase( "FRH" )]
        [TestCase( "FRI" )]
        public void TestSimpleMarshaling( string functionName ) {
            MethodInfo methodInfo = _compiledInstance.GetType().GetMethods().Single( x => x.Name.Equals( "f_"+functionName ) );
            var result = (Variant) methodInfo.Invoke( _compiledInstance, null );
            Assert.IsTrue( result );
        }
    }
}
