Func Dto2Dscreencoords($x, $y, $z, $ScreenWidth, $ScreenHeight)
	Local $return[2]
	$return[0] = $ScreenWidth / 2 + ($x / $z)
	$return[1] = $ScreenHeight / 2 + ($y / $z)
	Return $return
EndFunc   ;==>CalcCoordinates

Func AngleToRad($Angle)
	Return 2 * 3.141592653589793234 * $Angle / 360
EndFunc   ;==>AngleToRad

Func _Angle($X, $Y,$CX,$CY) ;nicht selber geschrieben, sondern aus dem internet ge-copy&pasted.
	If ($CX = $X) And ($CY = $Y) Then
		Return SetError(1, 0, 1)
	EndIf
	Local $Grad = ($Y <= $CY) * 180 - ATan(($CX - $X) / ($CY - $Y)) * 180 / 3.1415926535897932384626
	If $Grad = 360 Then
		$Grad = 0
	EndIf
	Return $Grad
 EndFunc   ;==>_Angle
