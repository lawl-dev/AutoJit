using System;
using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    public static class WinApiCalls
    {
        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern bool PeekConsoleInput( IntPtr hConsoleInput, out InputRecord buffer, int numInputRecords_UseOne, out int numEventsRead );

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern IntPtr GetStdHandle( int nStdHandle );
    }
}
