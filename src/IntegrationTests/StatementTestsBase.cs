using System;
using System.Diagnostics;
using System.Threading;

namespace UnitTests
{
    public class StatementTestsBase
    {
        public int ExecuteScript( string file ) {
            string filePath = string.Format( "{0}..\\..\\..\\testdata\\Statements\\{1}", Environment.CurrentDirectory, file );
            string au3ExePath = string.Format( "{0}..\\..\\..\\testdata\\Statements\\Autoit3.exe", Environment.CurrentDirectory );
            Process process = Process.Start( au3ExePath, string.Format( "/AutoIt3ExecuteScript {0}", filePath ) );
            while( !process.HasExited ) {
                Thread.Sleep( 10 );
            }
            return process.ExitCode;
        }
    }
}
