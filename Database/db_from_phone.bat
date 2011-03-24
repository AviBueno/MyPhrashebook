@echo off
echo Are you sure you want to copy mpb.db FROM the phone? (CTRL+C to abort)
pause
echo.

move mpb.db mpb.db.bak > nul 2>&1
%ANDROID_TOOLS%\adb pull /data/data/skydiver.dev/databases/mpb.db mpb.db

echo.
echo Done.
pause