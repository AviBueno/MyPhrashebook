@echo off
echo Are you sure you want to copy mpb.db FROM the phone? (CTRL+C to abort)
pause
echo.

move mpb.db mpb.db.bak > nul 2>&1

REM "adb -d" will redirect the command to a single connected USB device (even if an emulator is also running)
%ANDROID_TOOLS%\adb -d pull /data/data/skydiver.dev/databases/mpb.db mpb.db

echo.
echo Done.
pause