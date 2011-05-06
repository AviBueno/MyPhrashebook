@echo off
echo Are you sure you want to copy mpb.db TO the phone? (CTRL+C to abort)
pause
echo.

REM "adb -d" will redirect the commands to a single connected USB device (even if an emulator is also running)
%ANDROID_TOOLS%\adb -d shell "mv /data/data/skydiver.dev/databases/mpb.db /data/data/skydiver.dev/databases/mpb.db.bak"
%ANDROID_TOOLS%\adb -d push mpb.db /data/data/skydiver.dev/databases

echo.
echo Done.
pause
