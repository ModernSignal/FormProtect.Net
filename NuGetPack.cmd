echo off
rem delete existing nuget packages
del FormProtectNet.*.nupkg
rem Pack and Push the FormProtectNet NuGet package
..\nuget pack FormProtectNet\FormProtectNet.csproj -Build
..\nuget push FormProtectNet.*.nupkg -s https://www.nuget.org
pause