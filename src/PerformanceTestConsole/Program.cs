using AutoJITRuntime;
using AutoJITScript;

namespace PerformanceTestConsole
{
    internal class Program
    {
        private static void Main( string[] args ) {
            var autoJITScriptClass = new AutoJITScriptClass();
            for ( int i = 0; i < 100; i++ ) {
                Variant fWinApiEnumWindows = autoJITScriptClass.f__WinAPI_EnumWindows();
                for ( int j = 0; j < fWinApiEnumWindows[0, 0]; j++ ) {
                    Variant fWinApiEnumWindow = fWinApiEnumWindows[j, 0];
                    Variant winApiEnumWindow = fWinApiEnumWindows[j, 1];
                }
            }
        }
    }
}
