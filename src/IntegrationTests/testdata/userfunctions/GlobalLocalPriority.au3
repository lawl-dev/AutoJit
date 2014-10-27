$a = 1 ;assign with global scope

$res = Foo()
if $a <> 1 Then Exit 1
if $res <> 4 Then Exit 2

$res = Foo2()
if $a <> 2 Then Exit 3
if $res <> 4 Then Exit 4

$res = Foo3()
if $res <> 7 Then Exit 4

$res = Foo4()
if $res <> 3 Then Exit 4

$res = Foo5()
if $res <> 1338 Then Exit 4
$res = Foo5()
if $res <> 1339 Then Exit 4
$res = Foo5()
if $res <> 1340 Then Exit 4

Func Foo()
$b = $a + 1
Local $a = 2 ;a with local scope
$b += $a
Return $b
EndFunc

Func Foo2()
$b = $a + 1
Global $a = 2 ;a with global scope
$b += $a
Return $b
EndFunc


Func Foo3()
	Enum Step *2 $c, $d, $e
	Return $c + $d + $e
EndFunc

Func Foo4()
	Global Enum $c, $d, $e
	Return $c + $d + $e
EndFunc

Func Foo5()
	Local Static $a = 1337
	$a+=1
	Return $a
EndFunc


