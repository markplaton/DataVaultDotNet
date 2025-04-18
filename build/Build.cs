using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Solution] readonly Solution Solution;

    [Parameter("GitHub repo (e.g. user/repo)")] readonly string GitHubRepo;

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s.SetProject(Solution));
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .EnableNoBuild());
        });

    Target DocsDev => _ => _
        .Executes(() =>
        {
            ProcessTasks.StartProcess("pnpm", "docs:dev", workingDirectory: RootDirectory / "docs").AssertWaitForExit();
        }
        );

    Target DocsBuild => _ => _
        .Executes(() =>
        {
            ProcessTasks.StartProcess("pnpm", "docs:build", workingDirectory: RootDirectory / "docs").AssertWaitForExit();
        });

    Target DocsServe => _ => _
        .DependsOn(DocsBuild)
        .Executes(() =>
        {
            ProcessTasks.StartProcess("pnpm", "docs:serve", workingDirectory: RootDirectory / "docs").AssertWaitForExit();
        });

    Target DocsDeploy => _ => _
        .DependsOn(DocsBuild)
        .Executes(() =>
        {
            var outputDir = RootDirectory / "docs" / ".vitepress" / "dist";

            ProcessTasks.StartProcess("git", "init", outputDir).AssertZeroExitCode();
            ProcessTasks.StartProcess("git", "checkout -b gh-pages", outputDir).AssertZeroExitCode();
            ProcessTasks.StartProcess("git", "add .", outputDir).AssertZeroExitCode();
            ProcessTasks.StartProcess("git", "commit -m \"Deploy docs\"", outputDir).AssertZeroExitCode();
            ProcessTasks.StartProcess("git", $"push -f https://github.com/{GitHubRepo}.git gh-pages", outputDir).AssertZeroExitCode();


        });
}
