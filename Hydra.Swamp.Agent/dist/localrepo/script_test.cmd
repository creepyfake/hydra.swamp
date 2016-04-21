REM Script di test
@ECHO OFF
SET me=%~n0
SET exe_directory=%~dp0
REM -------------------

ECHO eseguo %me%

SET foo=bar

SET first_par=%1
echo PRIMO PARAMETRO %first_par%
SET second_par=%2
echo SECONDO PARAMETRO %second_par%

SET crash_par=%3
echo CRASH PARAMETRO %crash_par%

SET nowait_par=%4
echo NOWAIT %nowait_par%

IF "%crash_par%"=="" (SET crash_par=false)

ECHO FOO = %foo%

IF "%crash_par%"=="true" (
	ECHO requested crash 1>&2
	EXIT 1
	)

timeout 5