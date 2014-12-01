$x = 10


Global $AutoPropertyA
$backendField_propertyWithBackendField = ""


Func get_SquaredX()
	Return $x * $x
EndFunc   ;==>get_SquaredX

Func set_PropertyWithBackendField($value_38c0038472eb45d18fcf30589492d599)
	$backendField_propertyWithBackendField = $value_38c0038472eb45d18fcf30589492d599
EndFunc   ;==>set_PropertyWithBackendField

Func get_PropertyWithBackendField()
	Return $backendField_propertyWithBackendField
EndFunc   ;==>get_PropertyWithBackendField

Func IUseTheSquaredXProperty()
	$x += 1
	Return get_SquaredX()
EndFunc   ;==>IUseTheSquaredXProperty

Func IUseTheAutoPropertyA()
	Return $AutoPropertyA
EndFunc   ;==>IUseTheAutoPropertyA

Func IPropertyWithBackendField()
	$PropertyWithBackendField = "lol"
	Return PropertyWithBackendField
EndFunc   ;==>IPropertyWithBackendField


