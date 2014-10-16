Global $Chrono[21] ; Chronometer values

Global $Mess = "Timer values"&@CRLF ; String to contain MsgBox content.

; Actual timing loops
; ============================================================================
For $i = 1 To 20 ; 20 iterations of set
	$go = TimerInit() ; Start your engines!

	For $j = 1 To 30
		getPrimesTo(1337)
	Next ; $j

	$Chrono[$i] = TimerDiff($go) ; Ok, how long did it take?
	$Mess &= "Pass "&$i&" = "&$Chrono[$i]&"ms"&@CRLF ; Jolt it down for the report
Next ; $i
; ============================================================================

_Report() ; ... err report it!
Sleep(10000)

Exit


Func getPrimesTo($limit)
	Local $t[$limit+1], $p[$limit+1] = [0]
	For $a = 2 To $limit
		If $t[$a] <> 1 Then
			If $a <= Sqrt($limit) Then
				For $b = 2*$a To $limit Step $a
					$t[$b] = 1
				Next
			EndIf
			$p[0]+=1
			$p[$p[0]]=$a
		EndIf
	Next
	ReDim $p[$p[0]+1]
	Return $p
EndFunc

; ==== FUNCTIONS =============================================================
Func _Report()
	$Mess &= @CRLF ; Add an empty line
	$Mess &= "Min: "&_Minn()&"ms"&@CRLF ; Find minimum value and add it
	$Mess &= "Max: "&_Maxx()&"ms"&@CRLF ; Find maximum value and add it
	$Mess &= "Ave: "&_Ave()&"ms"&@CRLF ; Calculate median value and add it!
	ConsoleWrite(ConsoleWrite($Mess & @CRLF) & @CRLF)
	;MsgBox(48,"Results",$Mess) ; Show it to the user --> see how @CRLF works?
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
	Local $i, $Ave = 0; Set local variables
	For $i = 1 To Ubound($Chrono) -1 ; Read all values in $Chrono, from 1 to end
		$Ave += $Chrono[$i] ; Add up all the values
	Next
	Return $Ave / $i ; Send back the value, dividing total by # of numbers added together --> average
EndFunc


Func F()
Local $arr[1][2]
EndFunc
