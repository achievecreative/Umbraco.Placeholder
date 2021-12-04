$UmbracoSite = 'D:\Projects\Others\BaseUmbraco'

dotnet build --output .\build\

Copy-Item -Path .\AC.Placeholder\App_Plugins\* -Destination "$($UmbracoSite)\App_Plugins\" -recurse -Force

Copy-Item -Path .\build\AC.Placeholder.* -Destination "$($UmbracoSite)\" -recurse -Force

Copy-Item -Path .\AC.Placeholder.Features\Views\* -Destination "$($UmbracoSite)\Views\" -recurse -Force