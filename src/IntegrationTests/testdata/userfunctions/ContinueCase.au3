Func Foo()
$i = 1
$i2 = 2
$i3 = 3
Switch $i
	Case 1
		ConsoleWrite("1")
		if $i2 = 2 Then ContinueCase
		ConsoleWrite("2")
		Return
	Case 2
		ConsoleWrite("3")
		Switch $i3
			Case 1
				ConsoleWrite("4")
			Case 2
				ConsoleWrite("5")
			case 3
				ConsoleWrite("6")
		EndSwitch
	Case 3
		ConsoleWrite("7")
	Case 4
		ConsoleWrite("8")
	Case 5
		ConsoleWrite("9")
EndSwitch
EndFunc