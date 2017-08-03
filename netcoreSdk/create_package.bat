dotnet restore Sdk.csproj
dotnet build Sdk.csproj -c:Release -f:netcoreapp1.1
..\nuget pack Package.nuspec