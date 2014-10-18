using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoJIT;
using AutoJIT.Compiler;
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
            var script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\KDMemory.au3", Environment.CurrentDirectory ) );

            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false ) );
        }

        [Test]
        public void Test_KDMemoryX() {
            var script = File.ReadAllText( "C:\\tests.au3" );
            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.WindowsApplication, false ) );
        }

        [Test]
        public void Test_algo() {
            var script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\algo.au3", Environment.CurrentDirectory ) );

            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.WindowsApplication, false ) );
        }

        [Test]
        public void Test_KDMemory_iflineblock() {
            var script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\KDMemory_iflineblock.au3", Environment.CurrentDirectory ) );

            Assert.DoesNotThrow( () => _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false ) );
        }

        [Test]
        public void Test_testscript1() {
            var script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\testscript1.au3", Environment.CurrentDirectory ) );
            var assembly = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );
            var asm = Assembly.Load( assembly );
            var type = asm.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            var dto2Screencoords = type.GetMethod( "f_Dto2Dscreencoords" );
            var angle = type.GetMethod( "f__Angle" );

            var instance = type.CreateInstanceWithDefaultParameters();

            var parameters = new object[] {
                Variant.Create( 1111 ),
                Variant.Create( 111 ),
                Variant.Create( 11 ),
                Variant.Create( 100000 ),
                Variant.Create( 100888 )
            };
            var resDto2Dscreencoords = (Variant) dto2Screencoords.Invoke( instance, parameters );
            Assert.AreEqual( resDto2Dscreencoords[0], 50101 );

            var parameters2 = new object[] { Variant.Create( 111 ), Variant.Create( 222 ), Variant.Create( 333 ), Variant.Create( 444 ) };
            var angleres = (Variant) angle.Invoke( instance, parameters2 );
            Assert.AreEqual( angleres, 135 );
        }

        [Test]
        public void Test_testscript2() {
            var script = File.ReadAllText( string.Format( "{0}..\\..\\..\\testdata\\testscript2.au3", Environment.CurrentDirectory ) );
            var assembly = _compiler.Compile( script, OutputKind.ConsoleApplication, false );
            var assembly2 = Assembly.Load( assembly );
            var type = assembly2.GetTypes().Single( x => x.Name == "AutoJITScriptClass" );
            var method = type.GetMethod( "f_DoWhileFor" );
        }
    }
}
