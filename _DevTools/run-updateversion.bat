@echo off

rem Update AssemblyInfo version 
rem Copyright (c) Davide Gironi, 2014

powershell -Command "& { [Console]::WindowWidth = 150; [Console]::WindowHeight = 50; Start-Transcript out.txt; Import-Module .\Tools\PSake\psake.psm1; Invoke-psake '.\Tools\AutoBuilder\AutoBuilder.ps1' -task UpdateVersion; Remove-Module psake; %*; Stop-Transcript; exit $lastexitcode}"
@if "%NOPAUSE%"=="1" (echo stop) else (pause)
exit %errorlevel%
