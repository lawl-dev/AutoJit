$prime_count=10000
Local $primes[$prime_count]

$time=TimerInit()
for $i=1 To $prime_count-1
    if isPrime($i) Then
        $primes[$i]=true
    EndIf
Next
ConsoleWrite("Time taken: "&TimerDiff($time)&" ms"&@CRLF)

$res = 0
For $i = 0 To $prime_count - 1
$res += $primes[$i]
Next
ConsoleWrite($res)


func isPrime($prime)
    $end=$prime/2
    $start=1
    $is_prime=true
    While $end>$start
        $start+=1
        if Mod($prime,$start)==0 Then
            $is_prime=false
            ExitLoop
        EndIf
    WEnd
    return $is_prime
EndFunc