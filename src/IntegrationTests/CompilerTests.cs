using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT.Compiler;
using AutoJIT.CompilerApplication;
using AutoJITRuntime;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class CompilerTests
    {
        private readonly ICompiler _compiler;

        public CompilerTests() {
            var componentContainer = new CompilerBootStrapper();
            _compiler = componentContainer.GetInstance<ICompiler>();
        }

        [Test]
        public void Test_KDMemory() {
            string path = string.Format( "{0}..\\..\\..\\testdata\\KDMemory.au3", Environment.CurrentDirectory );

            Assert.DoesNotThrow( () => Program.Compile( "/IN", path, "/OUT", path+".exe", "/CONSOLE", "/Opt" ) );
        }

        [Test]
        public void Test_KDMemoryX() {
            string script = File.ReadAllText( "C:\\tests.au3" );
            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.WindowsApplication, false ) );
        }

        [Test]
        public void Test_algo() {
            string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\algo.au3", Environment.CurrentDirectory ) );

            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.WindowsApplication, false ) );
        }

        [Test]
        public void Test_KDMemory_iflineblock() {
            string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\KDMemory_iflineblock.au3", Environment.CurrentDirectory ) );

            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false ) );
        }

        [Test]
        public void Test_testscript1() {
            string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\testscript1.au3", Environment.CurrentDirectory ) );
            byte[] assembly = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
            Assembly asm = Assembly.Load( assembly );
            Type type = asm.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            MethodInfo dto2Screencoords = type.GetMethod( "f_Dto2Dscreencoords" );
            MethodInfo angle = type.GetMethod( "f__Angle" );

            object instance = type.CreateInstanceWithDefaultParameters();

            var parameters = new object[] {
                Variant.Create( 1111 ),
                Variant.Create( 111 ),
                Variant.Create( 11 ),
                Variant.Create( 100000 ),
                Variant.Create( 100888 )
            };
            var resDto2Dscreencoords = (Variant)dto2Screencoords.Invoke( instance, parameters );
            Assert.AreEqual( resDto2Dscreencoords[0], 50101 );

            var parameters2 = new object[] {
                Variant.Create( 111 ),
                Variant.Create( 222 ),
                Variant.Create( 333 ),
                Variant.Create( 444 )
            };
            var angleres = (Variant)angle.Invoke( instance, parameters2 );
            Assert.AreEqual( angleres, 135 );
        }

        [Test]
        public void Test_testscript2() {
            string script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\testscript2.au3", Environment.CurrentDirectory ) );
            byte[] assembly = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
            Assembly assembly2 = Assembly.Load( assembly );
            Type type = assembly2.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            MethodInfo method = type.GetMethod( "f_DoWhileFor" );
        }
    }
}
