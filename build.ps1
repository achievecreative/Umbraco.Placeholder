$UmbracoSite = 'D:\Projects\Achievecreative\baseUmbraco\123'

$msBuildExe = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe' 
& "$($msBuildExe)" ".\AC.Placeholder.sln"

Copy-Item -Path .\AC.Placeholder\App_Plugins\* -Destination "$($UmbracoSite)\App_Plugins\" -recurse -Force

Copy-Item -Path .\AC.Placeholder.Features\bin\AC.Placeholder.* -Destination "$($UmbracoSite)\bin\" -recurse -Force

Copy-Item -Path .\AC.Placeholder.Features\Views\* -Destination "$($UmbracoSite)\Views\" -recurse -Force