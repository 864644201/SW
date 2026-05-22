cd /d %~dp0
if "%PROCESSOR_ARCHITECTURE%"=="x86" goto x86
if "%PROCESSOR_ARCHITECTURE%"=="AMD64" goto x64
exit

:x86
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\regasm.exe  /codebase BoltTool.Addin.dll
pause

:x64
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe  /codebase BoltTool.Addin.dll
pause