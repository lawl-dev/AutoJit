using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct InputRecord
    {
        internal short eventType;
        internal KeyEventRecord keyEvent;
    }
}