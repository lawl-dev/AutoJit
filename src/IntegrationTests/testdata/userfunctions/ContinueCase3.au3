if Example(1) <> 2958 Then Exit 1
Func Example($i)
	$i += 1
	$ret = ""
	Switch $i
		Case 1 To 2
			$ret += $i
			$ret += Example($i)
			if Mod($i, 2) <> 0 Then ContinueCase
		Case 2 to 4
			$ret += $i * 2
			$ret += Example($i)
			if Mod($i, 2) = 0 Then ContinueCase
		Case 4 To 7
			$ret += $i * 3
			$ret += Example($i)
			if Mod($i, 2) <> 0 Then ContinueCase
		case 8 To 10
			$ret += $i * 4
			$ret += Example($i)
			if Mod($i, 2) = 0 Then ContinueCase
		case 12 To 14
			$ret += $i * 5
			$ret += Example($i)
			if Mod($i, 2) <> 0 Then ContinueCase
		case 15 to 16
			$ret += $i * 6
			$ret += Example($i)
	EndSwitch
	Return $ret
EndFunc
