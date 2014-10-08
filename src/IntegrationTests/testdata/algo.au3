
Local $Marker = ChrW(45446), $TestFile, $File = @TempDir&"\38473984328.txt"

For $i = 1 To 100
    $TestFile &= $Marker&ChrW(Random(10, 10000, 1))&Chr(Random(1, 255,1))
Next

FileWrite($File, _RLE_Decompress($TestFile))

Local $String = FileRead($File)

$Compress = _RLE_Compress($String)
$Decompress = _RLE_Decompress($Compress)

ConsoleWrite("================================="&@LF)
ConsoleWrite(StringLen($String)&@LF&StringLen($Compress)&@TAB&StringLen($Decompress)&@LF)
ConsoleWrite("================================="&@LF)

$Open = FileOpen($File&".cmp.txt", 2+16)
FileWrite($Open, StringToBinary($Compress))
FileClose($Open)

$Open = FileOpen($File&".dcmp.txt", 2+16)
FileWrite($Open, StringToBinary($Decompress))
FileClose($Open)

If $Decompress == $String Then MsgBox(48, 'Decompressed!', 'Decompressed string matches the original string >:D')

;===EndExample===;




;===============================================================================
;
; Function Name: _RLE_Compress
; Description:: Compresses data using the basic Run Length Encoding algorithm
; Parameter(s):
; $String - The string to compress
; Requirement(s): A brain
; Return Value(s): Success: The compressed string
; Error: I don't know if this can error :S
; Author(s): SxyfrG
;
; Link : <a href='http://www.binaryessence.com/dct/en000045.htm' class='bbc_url' title='External link' rel='nofollow external'>http://www.binaryessence.com/dct/en000045.htm</a> (doesn't explain it very well, but i worked out what to do :P)
; Example : Yes
;===============================================================================

Func _RLE_Compress($String);max length for a repeating section is 65536 due to unicode character limits ...
    Local $Marker = ChrW(45446), $CompressedString, $Count = 1, $Split = StringSplit($String, "")
;$Prog = ProgressOn("RLE_Compress", "Compressing Data", "0%")
    For $i = 1 To $Split[0]
        If $i <> $Split[0] Then;not the last byte
            If $i+$Count+$Count < $Split[0] Then
                If $Split[$i] == $Split[$i+$Count] And $Split[$i] == $Split[$i+$Count+$Count] And Asc($Split[$i]) <> Asc($Marker) Then;three bytes in a row that match and are not the marker bytes
                    While $Split[$i] == $Split[$i+$Count] And $i+$Count < $Split[0]
                        If $i+$Count > $Split[0] Then ExitLoop
                        $Count += 1
                    WEnd
                    $CompressedString &= $Marker&ChrW($Count-1)&$Split[$i];Add a marker byte, a unicode byte containing the repitition count and then the actual byte
                    $i += $Count-1;move $i forward appropriately
                    $Count = 1;reset the count
                Else
                    $CompressedString &= $Split[$i];no compression needed, continue building the string
                EndIf
            Else
                $CompressedString &= $Split[$i];no compression needed, continue building the string
            EndIf
        Else
            $CompressedString &= $Split[$i];no compression needed, continue building the string
        EndIf
    ;If Mod($i, 100) = 0 Then ProgressSet($i/$Split[0]*100, Round($i/$Split[0]*100)&"%")
    Next    
    
;ProgressOff()
    
    Return $CompressedString
EndFunc  ;==>_RLE_Compress

;===============================================================================
;
; Function Name: _RLE_Decompress
; Description:: Decompresses data using the basic Run Length Encoding algorithm
; Parameter(s):
; $String - The string to compress
; Requirement(s): A brain
; Return Value(s): Success: The Decompressed string
; Error: I don't know if this can error :S
; Author(s): SxyfrG
;
; Link : <a href='http://www.binaryessence.com/dct/en000045.htm' class='bbc_url' title='External link' rel='nofollow external'>http://www.binaryessence.com/dct/en000045.htm</a> (doesn't explain it very well, but i worked out what to do :P)
; Example : Yes
;===============================================================================

Func _RLE_Decompress($String)
    Local $Marker = ChrW(45446), $DecompressedString, $Split = StringSplit($String, "")
    
;ProgressOn("RLE_Decompress", "Decompressing Data", "0%")
    For $i = 1 To $Split[0]
        If $i <> $Split[0] And $i < $Split[0] Then;not the last byte
            If $Split[$i] = $Marker Then;marker byte found, check for other marker bytes
                If $i + 2 <= $Split[0] Then;safe to do array checks
                        For $v = 1 To AscW($Split[$i+1])
                            $DecompressedString &= $Split[$i+2]
                        Next
                        $i += 1
                Else
                    $DecompressedString &= $Split[$i];no compression made, continue building the string
                EndIf
            Else
                $DecompressedString &= $Split[$i];no compression made, continue building the string
            EndIf
        Else
            $DecompressedString &= $Split[$i];no compression made, continue building the string
        EndIf
    ;If Mod($i, 100) = 0 Then ProgressSet($i/$Split[0]*100, Round($i/$Split[0]*100)&"%")
    Next
;ProgressOff()
    
    Return StringReplace($DecompressedString, ChrW(45446), "");strip any marker bytes if any are left
EndFunc  ;==>_RLE_Decompress