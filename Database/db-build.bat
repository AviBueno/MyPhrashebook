IF "%1"=="" goto DEFAULT_DB
set MPB_DB=%1
goto CONT

:DEFAULT_DB
set MPB_DB=mpb.db
goto CONT

:CONT
IF EXIST %MPB_DB% del /q %MPB_DB%
sqlite3 %MPB_DB% < %MPB_DB%.txt
GOTO END

:END