@echo off
chcp 65001 >nul 2>&1
set "SCRIPTDIR=%~dp0"

echo.
echo ================================================
echo   NUnitAppium - LambdaTest TestMu AI Runner
echo   Runtime: .NET 10
echo ================================================
echo.

REM ── STEP 0: Check credentials ────────────────────────────────
if "%LT_USERNAME%"=="" goto :no_user
if "%LT_ACCESS_KEY%"=="" goto :no_key
echo [OK] Credentials set for user: %LT_USERNAME%
goto :check_dotnet
:no_user
echo [ERROR] LT_USERNAME is not set!
echo.
echo   Run these in this CMD window first, then run-tests.bat again:
echo     set LT_USERNAME=your_username_here
echo     set LT_ACCESS_KEY=your_access_key_here
echo.
pause
exit /b 1
:no_key
echo [ERROR] LT_ACCESS_KEY is not set!
echo.
echo   Run this first:  set LT_ACCESS_KEY=your_access_key_here
echo.
pause
exit /b 1

REM ── STEP 1: Check .NET 10 SDK ────────────────────────────────
:check_dotnet
echo.
echo [1/3] Checking .NET SDK...
where dotnet >nul 2>&1
if errorlevel 1 goto :no_dotnet
for /f "tokens=*" %%V in ('dotnet --version 2^>nul') do set "DOTNET_VER=%%V"
echo [OK] dotnet found: %DOTNET_VER%
echo %DOTNET_VER% | findstr /b "10." >nul
if not errorlevel 1 goto :restore
echo [WARN] Found dotnet %DOTNET_VER% but .NET 10 is preferred.
echo        Continuing anyway - if build fails, install .NET 10 SDK.
goto :restore
:no_dotnet
echo [ERROR] .NET SDK not found!
echo.
echo   Install .NET 10 SDK from:
echo   https://dotnet.microsoft.com/download/dotnet/10.0
echo.
echo   After install, close this window, open a NEW CMD and run again.
echo.
pause
exit /b 1

REM ── STEP 2: Restore + Build ──────────────────────────────────
:restore
echo.
echo [2/3] Restoring packages and building...
dotnet build "%SCRIPTDIR%NUnitAppium.sln" --configuration Release --nologo
if errorlevel 1 goto :build_fail
echo [OK] Build succeeded.
goto :run
:build_fail
echo.
echo [ERROR] Build failed. Common causes:
echo   - .NET 10 SDK not installed (install from https://dotnet.microsoft.com/download/dotnet/10.0)
echo   - No internet for NuGet package restore
echo.
pause
exit /b 1

REM ── STEP 3: Run tests ────────────────────────────────────────
:run
echo.
echo [3/3] Running tests...
echo.
echo ------------------------------------------------
echo   Username : %LT_USERNAME%
echo   Devices  : Check NUnitAppiumTests.cs [TestFixture] lines
echo   Dashboard: https://appautomation.lambdatest.com/build
echo ------------------------------------------------
echo.
echo   Currently configured devices (from NUnitAppiumTests.cs):
echo   Android  : Galaxy S24  ^| Android 14
echo   iOS      : iPhone 15   ^| iOS 17  (uncomment to enable)
echo ------------------------------------------------
echo.
dotnet test "%SCRIPTDIR%NUnitAppium.sln" --configuration Release --no-build --nologo --logger "console;verbosity=normal"
echo.
echo ================================================
echo   Done! View results at:
echo   https://appautomation.lambdatest.com/build
echo ================================================
echo.
pause
