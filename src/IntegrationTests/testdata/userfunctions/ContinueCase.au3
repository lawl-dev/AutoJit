$i = GetValue()
$i2 = GetValue()
$i3 = GetValue()

Switch $i2
	Case 1
		if $i3 = 1337 Then ContinueCase
		Switch $i
	Case 1
		ConsoleWrite("Code1")
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
	Case 4
		Switch $i
	Case 1
		ConsoleWrite("Code1")
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



Func GetValue()
	Return 1
EndFunc
