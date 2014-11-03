
$userfunc = UserFunc;
$userfuncbyref = UserFuncByRef;
$buildin = StringLen;



$ur = $userfunc(1, 2, 3)



$a = 1
$b = 2
$c = 3

$userfuncbyref($a, $b, $c)
$br = $buildin("Hallo")

If $ur <> 6 Then Exit 1
if $br <> 5 Then exit 2
if $a <> 2 Or $b <> "Hallo" Or $c <> 4 Then exit 3

Func UserFunc($a, $b, $c)
	Return $a+$b+$c;
EndFunc


Func UserFuncByRef(ByRef $a, ByRef $b, ByRef $c)
$a+=1
$b = "Hallo"
$c+=1
EndFunc
