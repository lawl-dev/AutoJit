using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto )]
    internal struct KeyEventRecord
    {
        internal bool keyDown;
        internal short repeatCount;
        internal short virtualKeyCode;
        internal short virtualScanCode;
        internal char uChar;
        internal int controlKeyState;
    }
}
