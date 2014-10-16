$path = "C:\Users\Brunnmeier\Documents\PrivateGIT\OPENSOURCE\Autojit\src\Release\UnmanagedTestDll.dll"

ConsoleWrite(FRA() & @CRLF)

Func FA()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncA", "BYTE", 2, "BOOLEAN", 1, "SHORT", 1)
Return $res[0]
EndFunc

Func FB()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncB", "USHORT", 3, "WORD", 4, "INT", 5)
Return $res[0]
EndFunc

Func FC()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncC", "LONG", 6, "BOOL", 1, "UINT", 7)
Return $res[0]
EndFunc

Func FD()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncD", "ULONG", 8, "DWORD", 9, "INT64", 10)
Return $res[0]
EndFunc

Func FE()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncE", "UINT64", 11, "INT*", 12, "HWND", WinGetHandle("Unmanaged.exe"))
Return $res[0]
EndFunc

Func FF()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncF", "HANDLE", WinGetHandle("Unmanaged.exe"), "FLOAT", 0.1, "DOUBLE", 0.1)
Return $res[0]
EndFunc

Func FG()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncG", "INT_PTR", 13, "LONG_PTR", 14, "LRESULT", 15)
Return $res[0]
EndFunc

Func FH()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncH", "LPARAM", 16, "UINT_PTR", 17, "ULONG_PTR", 18)
Return $res[0]
EndFunc

Func FI()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncI", "DWORD_PTR", 19, "WPARAM", 20, "STR", "0")
Return $res[0]
EndFunc

Func FJ()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncJ", "STR", "LOL", "PTR", 21)
Return $res[0]
EndFunc

Func FK()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncK", "LONG", 22, "INT64", 23, "INT64", 2425)
Return $res[0]
EndFunc

Func FL()
$handle = DllOpen($path)
$res = DllCall($handle, "BOOLEAN:cdecl", "TestFuncL", "UINT64", 26, "INT64", 2728, "ULONG_PTR", 29)
Return $res[0]
EndFunc









Func FRA()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefA", "BYTE*", 2, "BOOLEAN*", 1, "SHORT*", 1)
Return $res[1] = 3 and $res[2] = 0 and $res[3] = 2
EndFunc

Func FRB()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefB", "USHORT*", 3, "WORD*", 4, "INT*", 5)
Return $res[1] = 4 and $res[2] = 5 and $res[3] = 6
EndFunc

Func FRC()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefC", "LONG*", 6, "BOOL*", 1, "UINT*", 7)
Return $res[1] = 7 and $res[2] <> 1 and $res[3] = 8
EndFunc

Func FRD()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefD", "ULONG*", 8, "DWORD*", 9, "INT64*", 10)
Return $res[1] = 9 and $res[2] = 10 and $res[3] = 11
EndFunc

Func FRE()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefE", "UINT64*", 11, "INT*", 12)
Return $res[1] = 12 and $res[2] = 13
EndFunc

Func FRF()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefF", "FLOAT*", 0.1, "DOUBLE*", 0.1)
Return $res[1] >= 1.1 and $res[2] >= 1.1
EndFunc

Func FRG()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefG", "INT_PTR*", 13, "LONG_PTR*", 14, "LRESULT*", 15)
Return $res[1] = 14 and $res[2] = 15 and $res[3] = 16
EndFunc

Func FRH()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefH", "LPARAM*", 16, "UINT_PTR*", 17, "ULONG_PTR*", 18)
Return $res[1] = 17 and $res[2] = 18 and $res[3] = 19
EndFunc

Func FRI()
$handle = DllOpen($path)
$res = DllCall($handle, "NONE:cdecl", "TestFuncRefI", "DWORD_PTR*", 19, "WPARAM*", 20)
Return $res[1] = 20 and $res[2] = 21
EndFunc