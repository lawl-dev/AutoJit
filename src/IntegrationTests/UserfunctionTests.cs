using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoJIT.Compiler;
using AutoJITRuntime.Variants;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class UserfunctionTests
    {
        private readonly ICompiler _compiler;

        public UserfunctionTests() {
            var standardAutoJITContainer = new CompilerBootStrapper();
            _compiler = standardAutoJITContainer.GetInstance<ICompiler>();
        }

        [TestCase( "_WinAPI_GetIconInfo.au3" )]
        public void Test_compile_includes( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
                    var assembly = Assembly.Load( assemblyBytes );
                    var type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    var method = type.GetMethod( "f_Example" );
                    var instance = type.CreateInstanceWithDefaultParameters();
                    var res = method.Invoke( instance, null );
                } );
        }

        [TestCase( "_WinAPI_CreateFile.au3" )]
        public void Test_compile_includes3( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

                    var assembly = Assembly.Load( assemblyBytes );
                    var type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    var method = type.GetMethod( "f_Example" );
                    var instance = type.CreateInstanceWithDefaultParameters();
                } );
        }

        [TestCase( "_WinAPI_EqualMemory.au3" )]
        public void Test_compile_includes4( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

                    var assembly = Assembly.Load( assemblyBytes );
                    var type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    var method = type.GetMethod( "f_Example" );
                    var instance = type.CreateInstanceWithDefaultParameters();
                    var res = method.Invoke( instance, null );
                } );
        }

        [TestCase( "Benchmark.au3" )]
        public void Test_compile_Benchmark( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );
                    var process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "DES.au3" )]
        public void Test_compile_DES( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );

                    var process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "RC4.au3" )]
        public void Test_compile_RC4( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    var assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );

                    var process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "WinAPI.au3" )]
        public void Test_compile__WinAPI_GetCurrentProcessID( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow( () => { assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false ); } );

            var assembly = Assembly.Load( assemblyBytes );
            var type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            var instance = type.CreateInstanceWithDefaultParameters();
            var methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_GetCurrentProcessID" ) );
            var _WinAPI_FloatToInt = instance.GetType().GetMethods().Single(x => x.Name.Equals("f__WinAPI_FloatToInt"));
            var invoke = _WinAPI_FloatToInt.Invoke( instance, new[] { new DoubleVariant(1234.012335) } );


            var res = methodInfo.Invoke( instance, null );
        }


        [TestCase("WinAPITheme.au3")]
        public void Test_compile__WinAPI_GetCurrentThemeName(string file)
        {
            var path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            var script = File.ReadAllText(path);

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow(() => { assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false); });

            var assembly = Assembly.Load(assemblyBytes);
            var type = assembly.GetTypes().Single(x => x.Name == "AutoJITScriptClass");
            var instance = type.CreateInstanceWithDefaultParameters();
            var methodInfo = instance.GetType().GetMethods().Single(x => x.Name.Equals("f__WinAPI_GetCurrentThemeName"));
            var invoke = methodInfo.Invoke( instance, new object[0] );
        }

        [Test]
        public void Foo() {
            
        }
    }
}
