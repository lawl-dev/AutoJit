Func DoWhileFor($count)
$timer = TimerInit()
$i = 0
While $i < $count
$i = $i + 1
WEnd
return TimerDiff($timer)
EndFunc

ConsoleWrite(DoWhileFor(133742222))