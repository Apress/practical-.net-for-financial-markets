@echo off
copy C:\CodeExample\Chpt5\AppOperationEngine\OrderMatching\bin\Debug\OrderMatching.exe C:\CodeExample\Chpt5\AppOperationEngine\AppAgent\bin\Debug
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd