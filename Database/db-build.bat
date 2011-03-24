IF "%1"=="" goto ERROR
IF EXIST %1 del /q %1
sqlite3 %1 < %1.txt
GOTO END

:ERROR
echo Pleaes enter filename

:END