dotnet restore Sdk.csproj
dotnet build Sdk.csproj -c:Release
..\nuget pack Package.nuspec