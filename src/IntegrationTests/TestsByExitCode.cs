using System.Diagnostics;
using System.Threading;
using AutoJIT.CompilerApplication;
using NUnit.Framework;

namespace UnitTests
{
    public class TestsByExitCode
    {
        private const string Path = @"C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\IntegrationTests\testdata\userfunctions\";

        [TestCase( "ContinueCase2.au3" )]
        [TestCase( "ContinueCase3.au3" )]
        [TestCase( "GlobalLocalPriority.au3" )]
        [TestCase( "VariableFuncCall.au3" )]
        public void TestByExitCode( string fileName ) {
            string exePath = string.Format( "{0}{1}.exe", Path, fileName );

            Program.Compile( "/IN", Path+fileName, "/OUT", exePath, "/CONSOLE" );

            Process process = Process.Start( exePath );

            Assert.NotNull( process );

            while ( !process.HasExited ) {
                Thread.Sleep( 100 );
            }

            Assert.AreEqual( 0, process.ExitCode );
        }
    }
}
