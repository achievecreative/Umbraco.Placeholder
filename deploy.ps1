$UmbracoSite = 'D:\Projects\Others\Umbraco-8.0.2'

$msBuildExe = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe' 
& "$($msBuildExe)" ".\AC.Placeholder.sln"

Copy-Item .\AC.Placeholder.Features\bin\AC.Placeholder.* "$($UmbracoSite)\bin\"

Copy-Item .\AC.Placeholder.Features\Views\**\* "$($UmbracoSite)\Views"