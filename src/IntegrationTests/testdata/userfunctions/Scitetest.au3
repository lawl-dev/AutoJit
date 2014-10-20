#include <WinAPITheme.au3>

Global $Chrono[21] ; Chronometer values
Global $Mess = "Timer values"&@CRLF ; String to contain MsgBox content.

ConsoleWrite(__WINVER2())
Exit



Func __WINVER2()
	Local $tOSVI = DllStructCreate($tagOSVERSIONINFO)
	ConsoleWrite("S: " & DllStructGetSize($tOSVI) & @CRLF)
	DllStructSetData($tOSVI, 1, DllStructGetSize($tOSVI))

	Local $aRet = DllCall('kernel32.dll', 'bool', 'GetVersionExW', 'struct*', $tOSVI)
	If @error Or Not $aRet[0] Then Return SetError(@error, @extended, 0)
	Return BitOR(BitShift(DllStructGetData($tOSVI, 2), -8), DllStructGetData($tOSVI, 3))
EndFunc   ;==>__WINVER
Local $hui, $lum, $sat

; Actual timing loops
; ============================================================================
For $i = 1 To 20 ; 20 iterations of set
	$go = TimerInit() ; Start your engines!

	For $j = 1 To 10 ; 9999 iterations of commands her
		_WinAPI_GetCurrentThemeName()
	Next ; $j

	$Chrono[$i] = TimerDiff($go) ; Ok, how long did it take?
	$Mess &= "Pass "&$i&" = "&$Chrono[$i]&"ms"&@CRLF ; Jolt it down for the report

Next ; $i
; ============================================================================

_Report() ; ... err report it!
Sleep(10000)
Exit

; ==== FUNCTIONS =============================================================
Func _Report()
	$Mess &= @CRLF ; Add an empty line
	$Mess &= "Min: "&_Minn()&"ms"&@CRLF ; Find minimum value and add it
	$Mess &= "Max: "&_Maxx()&"ms"&@CRLF ; Find maximum value and add it
	$Mess &= "Ave: "&_Ave()&"ms"&@CRLF ; Calculate median value and add it!
	ConsoleWrite($Mess) ; Show it to the user --> see how @CRLF works?
EndFunc

Func _Maxx() ; Find maximum value and return it
	Local $i, $Max ; Set local variables
	For $i = 1 To Ubound($Chrono) -1 ; Read all values in $Chrono, from 1 to end
		If $Chrono[$i] > $Max Then $Max = $Chrono[$i] ; If the current value is larger than the current max, it is the new max.
	Next
	Return $Max ; Send back the value
EndFunc

Func _Minn() ; Find minimum value and add it
	Local $i, $Min = $Chrono[1] ; Set local variables. Notice $Min which equals the first value in $Chrono
	For $i = 1 To Ubound($Chrono) -1 ; Read all values in $Chrono, from 1 to end
		If $Chrono[$i] < $Min Then $Min = $Chrono[$i] ; If the current value is lower than the current min, it is the new min.
	Next
	Return $Min ; Send back the value
EndFunc

Func _Ave() ; Find average value and return it
	Local $i, $Ave ; Set local variables
	For $i = 1 To Ubound($Chrono) -1 ; Read all values in $Chrono, from 1 to end
		$Ave += $Chrono[$i] ; Add up all the values
	Next
	Return $Ave / $i ; Send back the value, dividing total by # of numbers added together --> average
EndFunc
