If Example(2, "") <> "Timeout Cancellation" Then Exit 1
If Example(1, "") <> "Cancellation" Then Exit 2

If Example(0, "a") <> "Vowel" Then Exit 3
If Example(0, "e") <> "Vowel" Then Exit 4
If Example(0, "i") <> "Vowel" Then Exit 5
If Example(0, "o") <> "Vowel" Then Exit 6
If Example(0, "u") <> "Vowel" Then Exit 7

If Example(0, "QP") <> "Mathematics" Then Exit 8
If Example(0, "QP") <> "Mathematics" Then Exit 9


If Example(0, "Q") <> "Science" Then Exit 9

If Example(0, "QA") <> "Science" Then Exit 10

If Example(0, "QB") <> "Science" Then Exit 11
If Example(0, "QC") <> "Science" Then Exit 12
If Example(0, "QD") <> "Science" Then Exit 13
If Example(0, "QE") <> "Science" Then Exit 14
If Example(0, "QF") <> "Science" Then Exit 15
If Example(0, "QG") <> "Science" Then Exit 16
If Example(0, "QH") <> "Science" Then Exit 17
If Example(0, "QI") <> "Science" Then Exit 18
If Example(0, "QJ") <> "Science" Then Exit 19
If Example(0, "QK") <> "Science" Then Exit 20
If Example(0, "QL") <> "Science" Then Exit 21
If Example(0, "QM") <> "Science" Then Exit 22
If Example(0, "QN") <> "Science" Then Exit 23
If Example(0, "QO") <> "Science" Then Exit 24
If Example(0, "QQ") <> "Science" Then Exit 25
If Example(0, "QR") <> "Science" Then Exit 26
If Example(0, "QS") <> "Science" Then Exit 27
If Example(0, "QT") <> "Science" Then Exit 28
If Example(0, "QU") <> "Science" Then Exit 29
If Example(0, "QV") <> "Science" Then Exit 30
If Example(0, "QW") <> "Science" Then Exit 31
If Example(0, "QX") <> "Science" Then Exit 32
If Example(0, "QY") <> "Science" Then Exit 33
If Example(0, "QZ") <> "Science" Then Exit 34



If Example(0, "QZA") <> "Others" Then Exit 35


If Example(0, "Z") <> "Others" Then Exit 36


Func Example($i, $n)
	$sMsg = ""
	Switch $i
		Case 2
			$sMsg = "Timeout "
			ContinueCase
		Case 1 ; Continuing previous case
			$sMsg &= "Cancellation"
		Case 0
			Switch $n
				Case "a", "e", "i", "o", "u"
					$sMsg = "Vowel"
				Case "QP"
					$sMsg = "Mathematics"
				Case "Q" To "QZ"
					$sMsg = "Science"
				Case Else
					$sMsg = "Others"
			EndSwitch
		Case Else
			$sMsg = "Something went horribly wrong."
	EndSwitch
	Return $sMsg
EndFunc   ;==>Example
