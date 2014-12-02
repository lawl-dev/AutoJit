Global $instanceIDs

Func InstanceCreate($name)
	Local $Instance[]
	Switch $name
		Case "MyClass"
			$Instance.FuncA = FuncA
			$Instance["FuncB"] = FuncB
			$Instance["Get_LocalB"] = Get_LocalB
			$Instance["InstanceID"] = $instanceIDs
			$instanceIDs += 1
	EndSwitch
	Return $Instance
EndFunc   ;==>InstanceCreate


$oMyClass1 = InstanceCreate("MyClass")
$oMyClass2 = InstanceCreate("MyClass")

For $i = 0 To 10
	$classMyClasssInstanceID = $oMyClass1.InstanceID
	$oMyClass1["FuncA"]($i)
Next
$classMyClasssInstanceID = $oMyClass1.InstanceID
ConsoleWrite($oMyClass1.Get_LocalB() & @CRLF)



For $i = 0 To 199
	$classMyClasssInstanceID = $oMyClass2.InstanceID
	$oMyClass2.FuncA($i)
Next
$classMyClasssInstanceID = $oMyClass1.InstanceID
ConsoleWrite($oMyClass1.Get_LocalB() & @CRLF)





;class MyClass
$classMyClasssInstanceID = 0
Local $locala[100]
Func FuncA($n)
	$locala[$classMyClasssInstanceID] = ($locala[$classMyClasssInstanceID]) + $n
EndFunc   ;==>FuncA

Local $localB[100][]

Func FuncB($n, $i)
	$localB[$classMyClasssInstanceID][$i] += $n
EndFunc   ;==>FuncB

Func Get_LocalB()
	Return $localB[$classMyClasssInstanceID]
EndFunc   ;==>Get_LocalB

;EndClass
