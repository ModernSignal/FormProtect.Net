echo off
rem Pack and Push the FormProtectNet NuGet package

rem Get version from AssemblyInfo.cs file
for /f "delims=() tokens=2" %%i in ('FINDSTR AssemblyVersion FormProtectNet\Properties\AssemblyInfo.cs') do set version=%%i
set version=%version:"=%
set version=%version:.*=%
echo Version %version%

..\nuget pack FormProtectNet\FormProtectNet.csproj -Version %version% -Build
..\nuget push FormProtectNet.%version%.nupkg -s https://www.nuget.org
pause