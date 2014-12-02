$x = 10
Property $SquaredX
	Get
		Return $x * $x
	EndGet
EndProperty
Property $AutoPropertyA
$backendField_propertyWithBackendField = ""
Property $PropertyWithBackendField
	Get
		Return $backendField_propertyWithBackendField
	EndGet
	Set
		$backendField_propertyWithBackendField = value
	EndSet
EndProperty
Func IUseTheSquaredXProperty()
	$x+=1
	Return $SquaredX
EndFunc
Func IUseTheAutoPropertyA()
	Return $AutoPropertyA
EndFunc
Func IUsePropertyWithBackendField()
	$PropertyWithBackendField = "lol"
	Return $PropertyWithBackendField
EndFunc
