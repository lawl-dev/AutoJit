$i = 1
$i2 = 2
$i3 = 3
$i4 = 4

Switch $i2
	Case 1
		If $i3 = 1337 Then ContinueCase
		Switch $i
			Case 1
				ConsoleWrite("Code1")
				ConsoleWrite("Code1")
				ConsoleWrite("Code1")
				If $i4 == 5 Then
					ContinueCase
				EndIf
				ConsoleWrite("Code1a")
				If $i4 == 6 Then
					Switch $i4
						Case 3
							ContinueCase
						Case 4
							ConsoleWrite("lol")
					EndSwitch
				EndIf
				ConsoleWrite("Code1a")
				ConsoleWrite("Code1a")
			Case 2
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				If $i2 = "0" Then ContinueCase
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
			Case 3
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
			Case Else
		EndSwitch
	Case 4
		Switch $i
			Case 1
				ConsoleWrite("Code4")
				ConsoleWrite("Code1")
				ConsoleWrite("Code1")
				ContinueCase
				ConsoleWrite("Code1a")
				ConsoleWrite("Code1a")
				ConsoleWrite("Code1a")
			Case 2
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				ConsoleWrite("Code2")
				If $i2 = "0" Then ContinueCase
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
				ConsoleWrite("Code2a")
			Case 3
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
				ConsoleWrite("Code3")
			Case Else
		EndSwitch
EndSwitch