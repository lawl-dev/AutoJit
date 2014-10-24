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
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
                    Assembly assembly = Assembly.Load( assemblyBytes );
                    Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    MethodInfo method = type.GetMethod( "f_Example" );
                    object instance = type.CreateInstanceWithDefaultParameters();
                    object res = method.Invoke( instance, null );
                } );
        }

        [TestCase( "_WinAPI_CreateFile.au3" )]
        public void Test_compile_includes3( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

                    Assembly assembly = Assembly.Load( assemblyBytes );
                    Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    MethodInfo method = type.GetMethod( "f_Example" );
                    object instance = type.CreateInstanceWithDefaultParameters();
                } );
        }

        [TestCase( "_WinAPI_EqualMemory.au3" )]
        public void Test_compile_includes4( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

                    Assembly assembly = Assembly.Load( assemblyBytes );
                    Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
                    MethodInfo method = type.GetMethod( "f_Example" );
                    object instance = type.CreateInstanceWithDefaultParameters();
                    object res = method.Invoke( instance, null );
                } );
        }

        [TestCase( "Benchmark.au3" )]
        public void Test_compile_Benchmark( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );
                    Process process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "DES.au3" )]
        public void Test_compile_DES( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );

                    Process process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "RC4.au3" )]
        public void Test_compile_RC4( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            Assert.DoesNotThrow(
                () => {
                    byte[] assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false );

                    File.WriteAllBytes( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe", assemblyBytes );

                    Process process = Process.Start( @"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.exe" );
                    while ( !process.HasExited ) {
                        Thread.Sleep( 1000 );
                    }
                    Debug.Write( process.ExitCode );
                } );
        }

        [TestCase( "WinAPI.au3" )]
        public void Test_compile__WinAPI_GetCurrentProcessID( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow( () => { assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false ); } );

            Assembly assembly = Assembly.Load( assemblyBytes );
            Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            object instance = type.CreateInstanceWithDefaultParameters();
            MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_GetCurrentProcessID" ) );
            MethodInfo _WinAPI_FloatToInt = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_FloatToInt" ) );
            object invoke = _WinAPI_FloatToInt.Invoke( instance, new[] { new DoubleVariant( 1234.012335 ) } );

            object res = methodInfo.Invoke( instance, null );
        }

        [TestCase( "WinAPITheme.au3" )]
        public void Test_compile__WinAPI_GetCurrentThemeName( string file ) {
            string path = string.Format( "{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file );
            string script = File.ReadAllText( path );

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow( () => { assemblyBytes = _compiler.Compile( script, OutputKind.ConsoleApplication, false ); } );

            Assembly assembly = Assembly.Load( assemblyBytes );
            Type type = assembly.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            object instance = type.CreateInstanceWithDefaultParameters();
            MethodInfo methodInfo = instance.GetType().GetMethods().Single( x => x.Name.Equals( "f__WinAPI_GetCurrentThemeName" ) );
            object invoke = methodInfo.Invoke( instance, new object[0] );
        }

        [TestCase("ContinueCase.au3")]
        public void Test_compile_ContinueCase(string file)
        {
            string path = string.Format("{0}..\\..\\..\\testdata\\userfunctions\\{1}", Environment.CurrentDirectory, file);
            string script = File.ReadAllText(path);

            var assemblyBytes = new byte[] { };
            Assert.DoesNotThrow(
                () => {
                    assemblyBytes = _compiler.Compile(script, OutputKind.ConsoleApplication, false);
                    var instanceWithDefaultParameters = Assembly.Load(assemblyBytes).GetTypes()[0].CreateInstanceWithDefaultParameters();
                    var invoke = instanceWithDefaultParameters.GetType().GetMethods().Single( x=>x.Name.Contains( "Foo" ) ).Invoke( instanceWithDefaultParameters, null );
                    File.WriteAllBytes( path + ".exe", assemblyBytes );
                });
        }

        [Test]
        public void Foo() {}
    }
}
