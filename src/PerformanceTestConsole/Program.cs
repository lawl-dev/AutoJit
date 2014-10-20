using System;
using AutoJITScript;

namespace PerformanceTestConsole
{
    internal class Program
    {
        private static void Main( string[] args ) {
            var autoJITScriptClass = new AutoJITScriptClass();
            for ( int i = 0; i < 100; i++ ) {
                var fWinApiEnumWindows = autoJITScriptClass.f__WinAPI_EnumWindows();
                for ( int j = 0; j < fWinApiEnumWindows[0,0]; j++ ) {
                    var fWinApiEnumWindow = fWinApiEnumWindows[j, 0];
                    var winApiEnumWindow = fWinApiEnumWindows[j, 1];
                }
            }
        }
    }
}
