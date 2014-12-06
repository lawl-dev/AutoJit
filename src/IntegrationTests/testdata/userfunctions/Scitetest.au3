#include <Array.au3>
;Class TestClass
;	Local $privateMember = 0
;	Local $privateMember2[100]
;	Func TestClassConstructor($_privateMember)
;		$privateMember = $_privateMember;
;	EndFunc
;	Func DoStuffWithPrivateMember()
;		$privateMember+=1
;	EndFunc
;	Func DoStuffWithPrivateMember2()
;		For $i = 0 To 200
;			$privateMember2[$i] +=1
;		Next
;	EndFunc
;	Func PrintPrivateMember()
;		ConsoleWrite($privateMember)
;	EndFunc
;
;	Func PrintPrivateMember2()
;		For $i = 0 To 200
;			ConsoleWrite($privateMember2[$i])
;		Next
;	EndFunc
;EndClass








$refCount = 0
$currentInstID = 0
Local $privateMember[0]
Local $privateMember2[0]
Func CreateInstance($name)
	Local $toReturn[]
	Switch $name
		Case "TestClass"
			$toReturn["CTOR"] = TestClassConstructor
			$toReturn["ID"] = $refCount
			$toReturn["DoStuffWithPrivateMember"] = DoStuffWithPrivateMember
			$toReturn["DoStuffWithPrivateMember2"] = DoStuffWithPrivateMember2
			$toReturn["PrintPrivateMember"] = PrintPrivateMember
			$toReturn["PrintPrivateMember2"] = PrintPrivateMember2
	EndSwitch
	$refCount += 1
	ReDim $privateMember[$refCount]
	ReDim $privateMember2[$refCount]
	Return $toReturn
EndFunc   ;==>CreateInstance




$instance = CreateInstance("TestClass")
$currentInstID = $instance["ID"]
$instance["CTOR"](1337)
$instance2 = CreateInstance("TestClass")
$currentInstID = $instance2["ID"]
$instance2["CTOR"](137)
$currentInstID = $instance["ID"]
$instance["DoStuffWithPrivateMember"]()
$currentInstID = $instance2["ID"]
$instance2["DoStuffWithPrivateMember"]()
$currentInstID = $instance2["ID"]
$instance2["DoStuffWithPrivateMember2"]()
$currentInstID = $instance["ID"]
$instance["PrintPrivateMember"]()
$currentInstID = $instance2["ID"]
$instance2["PrintPrivateMember"]()
$currentInstID = $instance2["ID"]
$instance2["PrintPrivateMember2"]()
Func TestClassConstructor($_privateMember)
	$privateMember[$currentInstID] = 0
	$privateMember[$currentInstID] = $_privateMember;
	Local $xyz[100]
	$privateMember2[$currentInstID] = $xyz
EndFunc   ;==>TestClassConstructor
Func DoStuffWithPrivateMember()
	$privateMember[$currentInstID] += 1
EndFunc   ;==>DoStuffWithPrivateMember
Func PrintPrivateMember()
	ConsoleWrite($privateMember[$currentInstID])
EndFunc   ;==>PrintPrivateMember
Func PrintPrivateMember2()
	For $i = 0 To 99
		ConsoleWrite(($privateMember2[$currentInstID])[$i])
	Next
EndFunc   ;==>PrintPrivateMember2
Func DoStuffWithPrivateMember2()
	For $i = 0 To 99
		$lolxyz = $privateMember2[$currentInstID]
		$lolxyz[$i] += 1
		$privateMember2[$currentInstID] = $lolxyz
	Next
EndFunc   ;==>DoStuffWithPrivateMember2



