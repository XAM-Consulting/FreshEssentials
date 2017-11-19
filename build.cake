//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "NugetBuild");
string solutionFilePath = "./src/FreshEssentials.sln";
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories ("./**/bin");
    CleanDirectories ("./**/obj");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFilePath);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild (solutionFilePath, c => {
        c.Configuration = configuration;
        c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
    });
});

Task("NuGet")
    .IsDependentOn("Build")
    .Does(() =>
{
    NuGetPack ("./FreshEssentials.nuspec",new NuGetPackSettings());    
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("NuGet");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);