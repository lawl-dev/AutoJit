
$userfunc = UserFunc;
$buildin = StringLen;


$ur = $userfunc(1, 2, 3)
$br = $buildin("Hallo")

If $ur <> 6 Then Exit 1
if $br <> 5 Then exit 2


Func UserFunc($a, $b, $c)
Return $a+$b+$c;
EndFunc

