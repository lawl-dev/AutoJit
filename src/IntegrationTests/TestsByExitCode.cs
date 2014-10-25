using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoJIT.Compiler;
using AutoJITRuntime;
using Lawl.Reflection;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class TestsByExitCode
    {
        private const string Path = @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\";
        


        [TestCase("ContinueCase2.au3")]
        [TestCase("ContinueCase3.au3")]
        public void TestByExitCode( string fileName ) {
            var exePath = string.Format( "{0}{1}.exe", Path, fileName );

            AutoJIT.CompilerApplication.Program.Compile("/IN", Path + fileName, "/OUT", exePath, "/CONSOLE" );

            var process = Process.Start( exePath );

            Assert.NotNull( process );

            while ( !process.HasExited ) {
                Thread.Sleep( 100 );
            }

            Assert.AreEqual(0, process.ExitCode);
        }
    }
}