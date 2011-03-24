@echo off
echo Are you sure you want to copy mpb.db TO the phone? (CTRL+C to abort)
pause
echo.

%ANDROID_TOOLS%\adb shell "mv /data/data/skydiver.dev/databases/mpb.db /data/data/skydiver.dev/databases/mpb.db.bak"
%ANDROID_TOOLS%\adb push mpb.db /data/data/skydiver.dev/databases

echo.
echo Done.
pause
