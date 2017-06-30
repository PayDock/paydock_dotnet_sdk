
#I "tools\FAKE\\tools"
#r @"FakeLib.dll"
open Fake.Testing
open Fake
open Fake.AssemblyInfoFile 
open System
open System.IO

// vars
let buildDir = @".\build\"
let srcDir = @".\src\"
let installerDir = @".\src\install\"
let installerOutputDir = @".\build\install\"
let configDir = getBuildParamOrDefault "configuration" "debug"
let testDir = @".\tests\"
let testResultsDir = @".\test-results"
let fxCopResultsDir = @".\fxcop-results\"
let packages = @".\packages\"
let buildServerVersion = 
    match buildServer with
        | Bamboo -> "0.0.0." + buildVersion
        | Jenkins -> "0.0.0." + buildVersion
        | _ -> "0.0.0.1"
let versionFromBuildParam = getBuildParamOrDefault "version" "Version build param is unavailable"
Console.WriteLine("BuildServerVersion: " + buildServerVersion)
Console.WriteLine("VersionFromBuildParam: " + versionFromBuildParam)
let version = getBuildParamOrDefault "version" buildServerVersion
let solutionDir = Path.Combine(Environment.CurrentDirectory, "src")
let libsOutputPath = (buildDir + @"libs\" + configDir)
let appsOutputPath = @".\build\apps\" + configDir
let azurePackageOutputPath = (buildDir + @"azure\" + configDir)
// tools
let nunitPath = @"./tools/NUnit.ConsoleRunner/tools/nunit3-console.exe"
let fxCopPath = @"./tools/FxCop.BuildTools/tools/FxCop/FxCopCmd.exe"

let isRunningOnCI = (buildServer = TeamCity || buildServer = Bamboo || buildServer = Jenkins);
Console.WriteLine("Running on CI server: " + isRunningOnCI.ToString())

Console.WriteLine ("Version Resolved by Script: " + version)
Console.WriteLine ("Configuration Resolved by Script: " + configDir)

let MSBuildWithProjectProperties outputPath (targets : string) (properties : (string) -> (string * string) list) projects = 
    let projects = projects |> Seq.toList
    
    let output = 
        if isNullOrEmpty outputPath then ""
        else 
        outputPath
          |> FullName
          |> trimSeparator

    let properties = 
        if isNullOrEmpty output then properties
        else fun x -> ("OutputPath", output) :: (properties x)

    let dependencies =
        projects 
            |> List.map getProjectReferences
            |> Set.unionMany

    let setBuildParam project projectParams = 
        { projectParams with Targets = targets |> split ';' |> List.filter ((<>) ""); NodeReuse = not isRunningOnCI; Properties = projectParams.Properties @ properties project }

    projects
      |> List.filter (fun project -> not <| Set.contains project dependencies)
      |> List.iter (fun project -> build (setBuildParam project) project)
    // it makes no sense to output the root dir content here since it does not contain the build output
    if isNotNullOrEmpty output then !!(outputPath @@ "/**/*.*") |> Seq.toList
    else []

let MSBuild outputPath targets properties projects = MSBuildWithProjectProperties outputPath targets (fun _ -> properties) projects

let BuildWebsite outputPath configuration solutionDirectory projectAppName projectFile  =

    let projectName = (fileInfo projectFile).Name.Replace(".csproj", "").Replace(".fsproj", "").Replace(".vbproj", "")  

    let slashes (dir : string) = 
        dir.Replace("\\", "/").TrimEnd('/')
        |> Seq.filter ((=) '/')
        |> Seq.length
    
    let currentDir = (directoryInfo ".").FullName    
    let projectDir = (fileInfo projectFile).Directory.FullName    
    let diff = slashes projectDir - slashes currentDir

    let prefix = if Path.IsPathRooted outputPath
                 then ""
                 else (String.replicate diff "../")    

    MSBuild "" "Rebuild" ["Configuration", configDir; "SolutionDir", solutionDirectory] [ projectFile ] |> ignore

    let outDir = prefix + outputPath

    let appName = if isNotNullOrEmpty projectAppName then projectAppName else projectName
    let webProjectOutputDir = prefix + outputPath + "/" + appName;            
    let copyPath  = outputPath + "/" + appName + "/bin/"    

    MSBuild "" "_WPPCopyWebApplication;_BuiltWebOutputGroupOutput" 
        [ "WebProjectOutputDir", webProjectOutputDir ] [ projectFile ]
        |> ignore

    let binaryContentPath = projectDir + "/bin/*.*"
    Console.WriteLine ("Binary Content Path: " + binaryContentPath)
    !!(binaryContentPath) |> Copy(copyPath)

    if isNotNullOrEmpty outputPath then !!(outputPath @@ "/**/*.*") |> Seq.toList
    else []

let capitalize input =
    let culture = System.Globalization.CultureInfo.GetCultureInfo("en-US")
    let titleCase = culture.TextInfo.ToTitleCase input
    titleCase

let RestorePackages() = 
    !! "./**/packages.config"    
    |> Seq.iter (RestorePackage (fun p -> {p with Sources = ["https://api.nuget.org/v3/index.json"; "https://www.nuget.org/api/v2"]}))

// targets
Target "Clean" (fun _ ->    
    CleanDirs [buildDir; testDir;]
)

Target "Restore Packages" (fun _ ->
    RestorePackages()
)

Target "Generate Solution Info" (fun _ ->
     CreateCSharpAssemblyInfo @".\src\SharedSolutionInfo\SolutionInfo.cs"
        [Attribute.Version version
         Attribute.FileVersion version
         Attribute.Product "Paydock.Net.Sdk"
         Attribute.Company "Paydock"
         Attribute.Copyright "Copyright © Paydock 2017. Warning: This computer program is protected by copyright law and international treaties. Unauthorized reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal penalties, and will be prosecuted to the maximum extent possible under the law."
         Attribute.Trademark "Ibox is a registered trademark of Paydock."
         Attribute.Configuration (capitalize configDir)
         Attribute.CLSCompliant true
         Attribute.ComVisible false
         ] 
)

Target "Compile Libs" (fun _ ->
  !! (srcDir + @"main\**\*.csproj")
  |> MSBuild libsOutputPath "Build" ["Configuration", configDir; "SolutionDir", solutionDir] 
  |> Log "Compile Libs:"      
)

Target "Compile Tests" (fun _ ->
    !! (srcDir + @"test\**\*.Tests.csproj") 
    |> MSBuild (testDir + configDir) "Build" ["Configuration", configDir; "SolutionDir", solutionDir] 
    |> Log "Compile Tests:"    
)

Target "Run Tests" (fun _ ->
    if not (Directory.Exists testResultsDir) then Directory.CreateDirectory testResultsDir |> ignore
    !! (testDir + configDir + @"\*.Tests.dll") |> NUnit3 (fun p -> { p with ToolPath = nunitPath;})
)

Target "Run FxCop" (fun () ->  
    if not (Directory.Exists fxCopResultsDir) then Directory.CreateDirectory fxCopResultsDir |> ignore
    !! (libsOutputPath + @"\**\Paydock.*.dll")   
    |> FxCop 
        (fun p -> 
            {p with                             
              ReportFileName = fxCopResultsDir + "FXCopResults.xml"
              IgnoreGeneratedCode = true
              UseGACSwitch = true
              ToolPath = fxCopPath})
)

// chains
"Clean"
    ==> "Restore Packages"
    ==> "Generate Solution Info"
    ==> "Compile Libs"
    //==> "Run FxCop"   
    ==> "Compile Tests"
    ==> "Run Tests"
    

Run "Run Tests"