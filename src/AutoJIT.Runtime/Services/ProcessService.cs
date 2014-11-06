using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using AutoJITRuntime.Exceptions;

namespace AutoJITRuntime.Services
{
    public class ProcessService
    {
        public Variant ShellExecute( Variant filename, Variant parameters, Variant workingdir, Variant verb, Variant showflag ) {
            var startInfo = new ProcessStartInfo( filename );

            if ( verb != string.Empty ) {
                startInfo.Verb = verb;
            }

            if ( workingdir != string.Empty ) {
                startInfo.WorkingDirectory = workingdir;
            }

            if ( parameters != string.Empty ) {
                startInfo.Arguments = parameters;
            }

            int flag = showflag.GetInt();
            if ( flag == 0 ) {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else if ( flag == 6 ) {
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            }
            else if ( flag == 3 ) {
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            }
            startInfo.UseShellExecute = true;

            try {
                Process process = Process.Start( startInfo );
                return process.Id;
            }
            catch (Win32Exception exception) {
                throw new ShellExcuteException( exception.NativeErrorCode, null, 0 );
            }
            catch (Exception) {
                throw new ShellExcuteException( 1, null, 0 );
            }
        }

        public Variant ShellExecuteWait( Variant filename, Variant parameters, Variant workingdir, Variant verb, Variant showflag ) {
            Variant pid = ShellExecute( filename, parameters, workingdir, verb, showflag );
            if ( pid ) {
                Process process = Process.GetProcessById( pid );
                while ( !process.HasExited ) {
                    Thread.Sleep( 10 );
                }
                return process.ExitCode;
            }
            return 0;
        }
    }
}
