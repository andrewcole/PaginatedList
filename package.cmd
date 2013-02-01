@echo off

set sdkdir=%~dp0tools\winsdk
set ngtdir=%~dp0.nuget
mkdir packages

"%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" /nologo /detailedsummary /p:Configuration=Release /t:Clean,Build
"%sdkdir%\signtool.exe" sign /a "Illallangi.PaginatedList\bin\Release\Illallangi.PaginatedList.dll"
"%sdkdir%\signtool.exe" timestamp /t http://timestamp.verisign.com/scripts/timstamp.dll "Illallangi.PaginatedList\bin\Release\Illallangi.PaginatedList.dll"
"%ngtdir%\NuGet.exe" pack Illallangi.PaginatedList\Illallangi.PaginatedList.csproj -Symbols -Verbosity detailed -Prop Configuration=Release -OutputDirectory packages

for %%f in (packages\*.symbols.nupkg) do "%ngtdir%\nuget.exe" push %%f -Source http://nuget.gw.symbolsource.org/MyGet/illallangi && del %%f
for %%f in (packages\*.nupkg) do "%ngtdir%\nuget.exe" push %%f -Source http://www.myget.org/F/illallangi/api/v2/package && del %%f