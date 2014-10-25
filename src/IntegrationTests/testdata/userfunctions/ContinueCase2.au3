if Example(2, "") <> "Timeout Cancellation" Then Exit 1
if Example(1, "") <> "Cancellation" Then Exit 2

if Example(0, "a") <> "Vowel" Then Exit 3
if Example(0, "e") <> "Vowel" Then Exit 4
if Example(0, "i") <> "Vowel" Then Exit 5
if Example(0, "o") <> "Vowel" Then Exit 6
if Example(0, "u") <> "Vowel" Then Exit 7

if Example(0, "QP") <> "Mathematics" Then Exit 8
if Example(0, "QP") <> "Mathematics" Then Exit 9


if Example(0, "Q") <> "Science" Then Exit 9

if Example(0, "QA") <> "Science" Then Exit 10

if Example(0, "QB") <> "Science" Then Exit 11
if Example(0, "QC") <> "Science" Then Exit 12
if Example(0, "QD") <> "Science" Then Exit 13
if Example(0, "QE") <> "Science" Then Exit 14
if Example(0, "QF") <> "Science" Then Exit 15
if Example(0, "QG") <> "Science" Then Exit 16
if Example(0, "QH") <> "Science" Then Exit 17
if Example(0, "QI") <> "Science" Then Exit 18
if Example(0, "QJ") <> "Science" Then Exit 19
if Example(0, "QK") <> "Science" Then Exit 20
if Example(0, "QL") <> "Science" Then Exit 21
if Example(0, "QM") <> "Science" Then Exit 22
if Example(0, "QN") <> "Science" Then Exit 23
if Example(0, "QO") <> "Science" Then Exit 24
if Example(0, "QQ") <> "Science" Then Exit 25
if Example(0, "QR") <> "Science" Then Exit 26
if Example(0, "QS") <> "Science" Then Exit 27
if Example(0, "QT") <> "Science" Then Exit 28
if Example(0, "QU") <> "Science" Then Exit 29
if Example(0, "QV") <> "Science" Then Exit 30
if Example(0, "QW") <> "Science" Then Exit 31
if Example(0, "QX") <> "Science" Then Exit 32
if Example(0, "QY") <> "Science" Then Exit 33
if Example(0, "QZ") <> "Science" Then Exit 34



if Example(0, "QZA") <> "Others" Then Exit 35


if Example(0, "Z") <> "Others" Then Exit 36


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
