REM Script di test
@ECHO OFF
SET me=%~n0
SET exe_directory=%~dp0
REM -------------------

SET workspace=#WORKSPACE

SET agent_dir=#AGENT_DIR

SET bin_dir=#BIN_DIR

SET data_dir=#DATA_DIR

SET modules_dir=#MODULES_DIR

SET scripts_dir=#SCRIPTS_DIR


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

ECHO installazione modulo #MODULE_NAME #MODULE_VERSION in #DESTINATION_DIR

ECHO usando artifatto #ARTIFACT_URL

REM crea la directory di destinazione
MKDIR #DESTINATION_DIR

IF "%crash_par%"=="true" (
	ECHO requested crash 1>&2
	EXIT 1
	)

timeout 5