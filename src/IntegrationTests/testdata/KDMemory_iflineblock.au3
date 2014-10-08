Global Const $__DELETE                                  = 0x00010000
Global Const $__READ_CONTROL                            = 0x00020000
Global Const $__WRITE_DAC                               = 0x00040000
Global Const $__WRITE_OWNER                             = 0x00080000
Global Const $__SYNCHRONIZE                             = 0x00100000

Global Const $__STANDARD_RIGHTS_READ                    = 0x00020000
Global Const $__STANDARD_RIGHTS_WRITE                   = $__STANDARD_RIGHTS_READ
Global Const $__STANDARD_RIGHTS_EXECUTE                 = $__STANDARD_RIGHTS_READ
Global Const $__STANDARD_RIGHTS_REQUIRED                = BitOR($__DELETE, $__READ_CONTROL, $__WRITE_DAC, $__WRITE_OWNER)
Global Const $__STANDARD_RIGHTS_ALL                     = BitOR($__STANDARD_RIGHTS_REQUIRED, $__SYNCHRONIZE)

Global Const $__ACCESS_SYSTEM_SECURITY                  = 0x01000000

Global Const $__PROCESS_TERMINATE                       = 0x0001
Global Const $__PROCESS_CREATE_THREAD                   = 0x0002
Global Const $__PROCESS_VM_OPERATION                    = 0x0008
Global Const $__PROCESS_VM_READ                         = 0x0010
Global Const $__PROCESS_VM_WRITE                        = 0x0020
Global Const $__PROCESS_DUP_HANDLE                      = 0x0040
Global Const $__PROCESS_CREATE_PROCESS                  = 0x0080
Global Const $__PROCESS_SET_QUOTA                       = 0x0100
Global Const $__PROCESS_SET_INFORMATION                 = 0x0200
Global Const $__PROCESS_QUERY_INFORMATION               = 0x0400
Global Const $__PROCESS_SUSPEND_RESUME                  = 0x0800
Global Const $__PROCESS_QUERY_LIMITED_INFORMATION       = 0x1000 ; Windows Server 2003 and Windows XP: This access right is not supported.
Global Const $__PROCESS_ALL_ACCESS_BS6000               = BitOR($__STANDARD_RIGHTS_REQUIRED, $__SYNCHRONIZE, 0x0FFF)
Global Const $__PROCESS_ALL_ACCESS_BB6000               = BitOR($__STANDARD_RIGHTS_REQUIRED, $__SYNCHRONIZE, 0xFFFF)
Global       $__PROCESS_ALL_ACCESS
If @OSBuild < 6000 Then
    $__PROCESS_ALL_ACCESS = $__PROCESS_ALL_ACCESS_BS6000
Else
    $__PROCESS_ALL_ACCESS = $__PROCESS_ALL_ACCESS_BB6000
EndIf

Global Const $TH32CS_SNAPHEAPLIST   = 0x01
Global Const $TH32CS_SNAPPROCESS    = 0x02
Global Const $TH32CS_SNAPTHREAD     = 0x04
Global Const $TH32CS_SNAPMODULE     = 0x08
Global Const $TH32CS_SNAPMODULE32   = 0x10
Global Const $TH32CS_INHERIT        = 0x80000000
Global Const $TH32CS_SNAPALL        = BitOR($TH32CS_SNAPHEAPLIST, $TH32CS_SNAPPROCESS, $TH32CS_SNAPTHREAD, $TH32CS_SNAPMODULE)


Func _KDMemory_OpenProcess($processId, $desiredAccess = $__PROCESS_ALL_ACCESS, $inheritHandle = 0)
    Local $handles[3], $callResult

    $handles[0] = $processId
    If Not ProcessExists($processId) Then Return SetError(1, 0, False)
	
	
    $handles[1] = DllOpen('Kernel32.dll')
    If $handles[1] == -1 Then 	Return SetError(2, 0, False)
	
	
    $callResult = DllCall($handles[1], 'ptr', 'OpenProcess', 'DWORD', $desiredAccess, 'BOOL', $inheritHandle, 'DWORD', $processId)
    If @error Then
        DllClose($handles[1])
        Return SetError(@error + 3, 0, False)
    ElseIf $callResult[0] == 0 Then
        DllClose($handles[1])
        Return SetError(9, 0, False)
    EndIf

    $handles[2] = $callResult[0]
    Return $handles
EndFunc

Func _KDMemory_CloseHandles($handles)
    Local $callResult

    If Not IsArray($handles) Then Return SetError(1, 0, False)
	
	
    $callResult = DllCall($handles[1], 'BOOL', 'CloseHandle', 'ptr', $handles[2])
    If @error Then Return SetError(@error + 1, 0, False)
    ElseIf $callResult[0] == 0 Then Return SetError(7, 0, False)
    DllClose($handles[1])
    Return True
EndFunc


Func _KDMemory_ReadProcessMemory($handles, $baseAddress, $type, $offsets = 0)
    Local $addressBuffer, $valueBuffer, $offsetsSize, $i, $callResult, $memoryData[2]

    If Not IsArray($handles) Then Return SetError(1, 0, False)
	
	
    $addressBuffer = DllStructCreate('ptr')
    If @error Then 	Return SetError(@error + 1, 0, False)
    
	
	DllStructSetData($addressBuffer, 1, $baseAddress)

    $valueBuffer = DllStructCreate($type)
    If @error Then 	Return SetError(@error + 5, 0, False)
	
	
    If IsArray($offsets) Then $offsetsSize = UBound($offsets)
    Else $offsetsSize = 0

    For $i = 0 To $offsetsSize
        If $i == $offsetsSize Then
            $callResult = DllCall($handles[1], 'BOOL', 'ReadProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($valueBuffer), 'ULONG_PTR', DllStructGetSize($valueBuffer), 'ULONG_PTR', 0)
            If @error Then Return SetError(@error + 20, $i, False)
            ElseIf $callResult[0] == 0 Then Return SetError(26, $i, False)
        Else
            $callResult = DllCall($handles[1], 'BOOL', 'ReadProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($addressBuffer), 'ULONG_PTR', DllStructGetSize($addressBuffer), 'ULONG_PTR', 0)
            If @error Then Return SetError(@error + 9, $i, False)
            ElseIf $callResult[0] == 0 Then Return SetError(15, $i, False)
        EndIf

        If $i < $offsetsSize Then
            DllStructSetData($addressBuffer, 1, DllStructGetData($addressBuffer, 1) + $offsets[$i])
            If @error Then Return SetError(@error + 15, $i, False)
		EndIf
    Next

    $memoryData[0] = DllStructGetData($addressBuffer, 1)
    $memoryData[1] = DllStructGetData($valueBuffer, 1)
    Return $memoryData
EndFunc


Func _KDMemory_ReadProcessString($handles, $baseAddress, $offsets = 0, $unicode = 0)
    Local $addressBuffer, $type, $size, $valueBuffer, $offsetsSize, $i, $callResult, $memoryData[2]

    If Not IsArray($handles) Then Return SetError(1, 0, False)
	
	
    $addressBuffer = DllStructCreate('ptr')
    If @error Then 	Return SetError(@error + 1, 0, False)
    
	
	DllStructSetData($addressBuffer, 1, $baseAddress)

    $memoryData[1] = ''
    If $unicode <> 1 Then
        $type = 'byte'
        $size = 1
    Else
        $type = 'short'
        $size = 2
    EndIf

    $valueBuffer = DllStructCreate($type)
    If @error Then Return SetError(@error + 5, 0, False)
    
	$size = DllStructGetSize($valueBuffer)

    If IsArray($offsets) Then $offsetsSize = UBound($offsets)
    Else $offsetsSize = 0
    

    For $i = 0 To $offsetsSize
        If $i == $offsetsSize Then
            $count = 0
            While True
                $callResult = DllCall($handles[1], 'BOOL', 'ReadProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($valueBuffer), 'ULONG_PTR', DllStructGetSize($valueBuffer), 'ULONG_PTR', 0)
                If @error Then
                    Return SetError(@error + 20, $i + $count, False)
                ElseIf $callResult[0] == 0 Then
                    Return SetError(26, $i + $count, False)
                EndIf

                $character = DllStructGetData($valueBuffer, 1)
                If $character == 0 Then
                    ExitLoop
                Else
                    If $unicode <> 1 Then
                        $memoryData[1] &= Chr($character)
                    Else
                        $memoryData[1] &= ChrW($character)
                    EndIf
                EndIf

                DllStructSetData($addressBuffer, 1, DllStructGetData($addressBuffer, 1) + $size)
                $count += 1
            WEnd
            DllStructSetData($addressBuffer, 1, DllStructGetData($addressBuffer, 1) - ($size * $count))
        Else
            $callResult = DllCall($handles[1], 'BOOL', 'ReadProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($addressBuffer), 'ULONG_PTR', DllStructGetSize($addressBuffer), 'ULONG_PTR', 0)
            If @error Then
                Return SetError(@error + 9, $i, False)
            ElseIf $callResult[0] == 0 Then
                Return SetError(15, $i, False)
            EndIf
        EndIf

        If $i < $offsetsSize Then
            DllStructSetData($addressBuffer, 1, DllStructGetData($addressBuffer, 1) + $offsets[$i])
            If @error Then 
			Return SetError(@error + 15, $i, False)
			EndIf
		EndIf
    Next

    $memoryData[0] = DllStructGetData($addressBuffer, 1)
    Return $memoryData
EndFunc


Func _KDMemory_WriteProcessMemory($handles, $baseAddress, $type, $value, $offsets = 0)
    Local $addressBuffer, $valueBuffer, $offsetsSize, $i, $callResult

    If Not IsArray($handles) Then 
	Return SetError(1, 0, False)
	EndIf
	
    $addressBuffer = DllStructCreate('ptr')
    If @error Then 
	Return SetError(@error + 1, 0, False)
    EndIf
	
	DllStructSetData($addressBuffer, 1, $baseAddress)

    $valueBuffer = DllStructCreate($type)
    If @error Then 
	Return SetError(@error + 5, 0, False)
	EndIf
	
    DllStructSetData($valueBuffer, 1, $value)
    If @error Then 
	Return SetError(@error + 9, 0, False)
	EndIf
	
    If IsArray($offsets) Then
        $offsetsSize = UBound($offsets)
    Else
        $offsetsSize = 0
    EndIf

    For $i = 0 To $offsetsSize
        If $i == $offsetsSize Then
            $callResult = DllCall($handles[1], 'BOOL', 'WriteProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($valueBuffer), 'ULONG_PTR', DllStructGetSize($valueBuffer), 'ULONG_PTR*', 0)
            If @error Then
                Return SetError(@error + 25, $i, False)
            ElseIf $callResult[0] == 0 Then
                Return SetError(31, $i, False)
            EndIf
        Else
            $callResult = DllCall($handles[1], 'BOOL', 'ReadProcessMemory', 'ptr', $handles[2], 'ptr', DllStructGetData($addressBuffer, 1), 'ptr', DllStructGetPtr($addressBuffer), 'ULONG_PTR', DllStructGetSize($addressBuffer), 'ULONG_PTR*', 0)
            If @error Then
                Return SetError(@error + 14, $i, False)
            ElseIf $callResult[0] == 0 Then
                Return SetError(20, $i, False)
            EndIf
        EndIf

        If $i < $offsetsSize Then
            DllStructSetData($addressBuffer, 1, DllStructGetData($addressBuffer, 1) + $offsets[$i])
            If @error Then 
			Return SetError(@error + 20, $i, False)
			EndIf
		EndIf
    Next

    Return DllStructGetData($addressBuffer, 1)
EndFunc


Func _KDMemory_WriteProcessString($handles, $baseAddress, $string, $offsets = 0, $unicode = 0)
    Local $type, $size, $stringLength, $result

    If $unicode <> 1 Then
        $type = 'CHAR'
        $size = 1
    Else
        $type = 'WCHAR'
        $size = 2
    EndIf

    $stringLength = StringLen($string)
    $type &= '[' & $stringLength & ']'

    $result = _KDMemory_WriteProcessMemory($handles, $baseAddress, $type, $string, $offsets)
    If @error Then 
	Return SetError(@error, @extended, False)
	EndIf
	
    _KDMemory_WriteProcessMemory($handles, $result + $size * $stringLength, 'byte', 0)
    If @error Then 
	Return SetError(32, @extended, False)
	EndIf
	
    Return $result
EndFunc


Func _KDMemory_GetModuleBaseAddress($handles, $moduleName, $caseSensitive = 0, $unicode = 0)
    Local $processId, $struct, $MODULEENTRY32, $callResult, $snapshot, $moduleBaseName, $moduleBaseAddress, $skip

    If Not IsArray($handles) Then 
	Return SetError(1, 0, False)
    EndIf
	
	If StringLen($moduleName) == 0 Then 
	Return SetError(2, 0, False)
	EndIf
    $struct = 'DWORD dwSize;DWORD th32ModuleID;DWORD th32ProcessID;DWORD GlblcntUsage;WORD ProccntUsage;ptr modBaseAddr;DWORD modBaseSize;ptr hModule;CHAR szModule[256];CHAR szExePath[260]'

    If $unicode == 1 Then 
	$struct = StringReplace($struct, 'CHAR', 'WCHAR')
    EndIF
	$MODULEENTRY32 = DllStructCreate($struct)
    If @error Then 
	Return SetError(@error + 2, 0, False)
	EndIf
	
    While True
        $callResult = DllCall($handles[1], 'ptr', 'CreateToolhelp32Snapshot', 'DWORD', BitOR($TH32CS_SNAPMODULE, $TH32CS_SNAPMODULE32), 'DWORD', $handles[0])
        If @error Then
            Return SetError(@error + 6, 0, False)
        ElseIf $callResult[0] = -1 Then
            $callResult = DllCall($handles[1], 'DWORD', 'GetLastError')
            If $callResult[0] == 0x18 Then 
			ContinueLoop ; ERROR_BAD_LENGTH = 0x18
            EndIf
			Return SetError(12, $callResult[0], False)
        Else
            ExitLoop
        EndIf
    WEnd

    $snapshot = $callResult[0]
    DllStructSetData($MODULEENTRY32, 'dwSize', DllStructGetSize($MODULEENTRY32))

    $callResult = DllCall($handles[1], 'BOOL', 'Module32First', 'ptr', $snapshot, 'ptr', DllStructGetPtr($MODULEENTRY32))
    If @error Then
        Return SetError(@error + 12, 0, False)
    ElseIf $callResult[0] == 0 Then
        $callResult = DllCall($handles[1], 'DWORD', 'GetLastError')
        Return SetError(18, $callResult[0], False)
    EndIf

    $skip = False
    While True
        If Not $skip Then
            If StringCompare(DllStructGetData($MODULEENTRY32, 'szModule'), $moduleName, $caseSensitive) == 0 Then
                Return DllStructGetData($MODULEENTRY32, 'hModule')
            EndIf
            $skip = False
        EndIf

        $callResult = DllCall($handles[1], 'BOOL', 'Module32Next', 'ptr', $snapshot, 'ptr', DllStructGetPtr($MODULEENTRY32))
        If @error Then
            Return SetError(@error + 18, 0, False)
        ElseIf $callResult[0] == 0 Then
            $callResult = DllCall($handles[1], 'DWORD', 'GetLastError')
            If $callResult[0] == 0x12 Then ; ERROR_NO_MORE_FILES = 0x12
                ExitLoop
            Else
                $skip = True
            EndIf
        EndIf
    WEnd

    DllCall($handles[1], 'BOOL', 'CloseHandle', 'ptr', $snapshot)
    Return SetError(24, 0, False)
EndFunc


Func _KDMemory_FindAddress($handles, $pattern, $startAddress, $endAddress, ByRef $errors, $getAll = 0)
    Local $size, $bytes, $errorListCount, $errorList[1][2], $addressListCount, $addressList[1], $memoryData, $offset

    If Not IsArray($handles) Then 
	Return SetError(1, 0, False)
    EndIf
	If $endAddress - $startAddress <= 0 Then 
	Return SetError(2, 0, False)
	EndIf
	
    $size = Int(StringLen($pattern) / 2) + 1
    $bytes = $size * 4

    $errorListCount = 0
    $errorList[0][0] = 0

    $addressListCount = 0
    $addressList[0] = 0

    For $address = $startAddress To $endAddress Step $size + 1
        $memoryData = _KDMemory_ReadProcessMemory($handles, $address, 'BYTE[' & $bytes & ']')
        If @error Then
            $errorListCount += 1
            ReDim $errorList[$errorListCount + 1][2]
            $errorList[$errorListCount][0] = $address
            $errorList[$errorListCount][1] = @error
            $errorList[0][0] = $errorListCount
        Else
            If StringLeft($memoryData[1], 2) = '0x' Then
                $memoryData[1] = StringTrimLeft($memoryData[1], 2)
            EndIf

            $pattern = StringRegExpReplace($pattern, '[^.0-9a-fA-F]', '')
            StringRegExp($memoryData[1], $pattern, 1)
            If Not @error Then
                $offset = Round((@extended - StringLen($pattern) - 2) / 2, 0)

                $addressListCount += 1
                ReDim $addressList[$addressListCount + 1]
                $addressList[$addressListCount] = $address + $offset
                $addressList[0] = $addressListCount

                If $getAll <> 1 Then 
					ExitLoop
				EndIF
            EndIf
        EndIf
    Next

    $errors = $errorList
    If $errorListCount > 0 Then 
	SetExtended(1)
	Endif
    Return $addressList
EndFunc