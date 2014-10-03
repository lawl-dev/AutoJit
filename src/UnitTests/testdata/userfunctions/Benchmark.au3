; #FUNCTION# ====================================================================================================================
; Name ..........: _StartBenchmark
; Description ...: checks how many rounds users cpu can produce in which time
; Syntax ........: _StartBenchmark($bRounds)
; Parameters ....: $bRounds = Rounds that get completed before func stops
; Return values .: Completed Rounds
; Author ........: FlutterShy
; Modified ......: /
; Remarks .......: /
; Related .......: /
; Link ..........: http://www.elitepvpers.com/forum/members/4721429--fluttershy-.html
; Example .......: _StartBenchmark(1000000)
; ===============================================================================================================================
Exit _StartBenchmark(13374242)


Func _StartBenchmark($bRounds)
	$complete = 0
	$bTimer = TimerInit()
	Do
		$complete = $complete + 1
	Until $complete = $bRounds
	$bDiff = TimerDiff($bTimer)
	Return $bDiff
EndFunc
; #FUNCTION# ====================================================================================================================
; Name ..........: _cpuBenchmark
; Description ...: checks how many rounds get completed in $cTime
; Syntax ........: _cpuBenchmark($cTime)
; Parameters ....: $cTime               - Time in MS
; Return values .: $rounds				- Rounds that get completed
; Author ........: FlutterShy
; Modified ......: /
; Remarks .......: /
; Related .......: /
; Link ..........: http://www.elitepvpers.com/forum/members/4721429--fluttershy-.html
; Example .......: _cpuBenchmark(30000)
; ===============================================================================================================================
Func _cpuBenchmark($cTime)
	$sTimer = TimerInit()
	$rounds = 0
	Do
		$rounds = $rounds + 1
		$Diff = TimerDiff($sTimer)
	Until $Diff > $cTime
	Return $rounds
EndFunc