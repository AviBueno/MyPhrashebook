IF "%1"=="" goto ERROR
sqlite3 %1 .dump > %1.txt
GOTO END

:ERROR
echo Pleaes enter filename

:END