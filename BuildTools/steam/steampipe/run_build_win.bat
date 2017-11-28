@echo off
setlocal

set SDK_VERSION=1.31
set SCRIPTS_DIR=%~dp0\scripts
set STEAM_CMD=%~dp0\..\sdk\%SDK_VERSION%\sdk\tools\ContentBuilder\builder\steamcmd.exe
set ACCOUNT=goingloudstudiosbuild

echo SDK Version: %SDK_VERSION%
echo Steam Command: %STEAM_CMD%
echo Scripts Dir: %SCRIPTS_DIR%
echo Account: %ACCOUNT%

if exist %STEAM_CMD% goto ENTER_PASSWORD
goto ERROR_SDK_NOT_FOUND

:ENTER_PASSWORD
set /p PASSWORD=Enter password: 

%STEAM_CMD% +login %ACCOUNT% %PASSWORD% +run_app_build_http %SCRIPTS_DIR%\lootquest\app_build_win_758500.vdf +quit

echo Build complete!

goto end

:ERROR_SDK_NOT_FOUND
set EXPECTED_SDK_DIR=..\sdk\%SDK_VERSION%
echo SDK Not found! Download the Steamworks SDK (version %SDK_VERSION%) and place it in %EXPECTED_SDK_DIR% so that this path exists: ..\sdk\%SDK_VERSION%\sdk\tools\ContentBuilder\builder\steamcmd.exe

:end
endlocal
pause