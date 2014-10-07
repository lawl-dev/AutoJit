
$timer = TimerInit()
For $i = 0 to 1337
$encrypted = _StringEncryptRC4("hallo liebe omi!", "acvdelsfkselfslea37834")
$decrypted = _StringDecryptRC4($encrypted, "acvdelsfkselfslea37834")
Next

$diff = TimerDiff($timer)
Exit $diff
;===============================================================================
;
; Function Name:   _StringEncryptRC4
; Description::    Encrypts text using RC4 Encryption
; Parameter(s):    $text, $encryptkey
; Requirement(s):  AutoIt
; Return Value(s): Encrypted String
; Author(s):       RazerM
;
;===============================================================================
;
Func _StringEncryptRC4($text, $encryptkey)
    Local $sbox[256]
    Local $key[256]
    Local $temp
    Local $a
    Local $i
    Local $j
    Local $k
    Local $cipherby
    Local $cipher

    $i = 0
    $j = 0

    __RC4Initialize($encryptkey, $key, $sbox)

    For $a = 1 To StringLen($text)
        $i = Mod(($i + 1),256)
        $j = Mod(($j + $sbox[$i]),256)
        $temp = $sbox[$i]
        $sbox[$i] = $sbox[$j]
        $sbox[$j] = $temp

        $k = $sbox[Mod(($sbox[$i] + $sbox[$j]),256)]

        $cipherby = BitXOR(Asc(StringMid($text, $a, 1)),$k)
        $cipher = $cipher & Chr($cipherby)
    Next

    Return _StringToHexEx($cipher)
EndFunc   ;==>_StringEncryptRC4

;===============================================================================
;
; Function Name:   _StringDecryptRC4
; Description::    Decrypts text using RC4 Encryption
; Parameter(s):    $text, $encryptkey
; Requirement(s):  AutoIt
; Return Value(s): Decrypted String
; Author(s):       RazerM
; Note(s):         RC4 uses the same algorithm to encrypt and decrypt
;
;===============================================================================
;

Func _StringDecryptRC4($text, $encryptkey)
    Local $sbox[256]
    Local $key[256]
    Local $temp
    Local $a
    Local $i
    Local $j
    Local $k
    Local $cipherby
    Local $cipher
    $text = _HexToStringEx($text)

    $i = 0
    $j = 0

    __RC4Initialize($encryptkey, $key, $sbox)

    For $a = 1 To StringLen($text)
        $i = Mod(($i + 1),256)
        $j = Mod(($j + $sbox[$i]),256)
        $temp = $sbox[$i]
        $sbox[$i] = $sbox[$j]
        $sbox[$j] = $temp

        $k = $sbox[Mod(($sbox[$i] + $sbox[$j]),256)]

        $cipherby = BitXOR(Asc(StringMid($text, $a, 1)),$k)
        $cipher = $cipher & Chr($cipherby)
    Next
    Return $cipher
EndFunc   ;==>_StringDecryptRC4


; Helper function
Func __RC4Initialize($strPwd, ByRef $key, ByRef $sbox)
    Dim $tempSwap
    Dim $a
    Dim $b

    $intLength = StringLen($strPwd)
    For $a = 0 To 255
        $key[$a] = Asc(StringMid($strPwd, (Mod($a,$intLength))+1, 1))
        $sbox[$a] = $a
    Next

    $b = 0
    For $a = 0 To 255
        $b = Mod($b + $sbox[$a] + $key[$a],256)
        $tempSwap = $sbox[$a]
        $sbox[$a] = $sbox[$b]
        $sbox[$b] = $tempSwap
    Next
EndFunc   ;==>__RC4Initialize

Func _HexToStringEx($strHex)
    Return BinaryToString("0x" & $strHex)
EndFunc   ;==>_HexToStringEx

Func _StringToHexEx($strChar)
    Return Hex(StringToBinary($strChar))
EndFunc