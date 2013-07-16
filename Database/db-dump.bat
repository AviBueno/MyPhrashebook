IF "%1"=="" goto DEFAULT_DB
set MPB_DB=%1
goto CONT

:DEFAULT_DB
set MPB_DB=mpb.db
goto CONT

:CONT
sqlite3 %MPB_DB% .dump > %MPB_DB%.txt
GOTO END

:END