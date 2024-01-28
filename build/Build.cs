using System.Text.Json;
using LiteDB;
using MexicoData.ZipCodes.Models;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Serilog;
using _build.Models;
using _build.Utils;

class Build : NukeBuild
{
    private List<ProjectDependencies> ProjectDependencies { get; set; }

    private AbsolutePath zipCodesCsvPath;

    public static int Main() => Execute<Build>(x => x.Compile);

    Target LoadProjectLock =>
        _ =>
            _.Executes(async () =>
            {
                ProjectDependencies = await LoadJsonAsync<List<ProjectDependencies>>(
                    RootDirectory / "resources.lock.json"
                );
                Log.Information(
                    "Loaded projects dependencies: {0}",
                    ProjectDependencies.Select(x => x.ProjectName)
                );
            });

    Target UpdateProjectLock =>
        _ =>
            _.DependsOn(LoadProjectLock)
                .Executes(async () =>
                {
                    bool hashChanges = false;
                    foreach (var project in ProjectDependencies)
                    {
                        foreach (var resource in project.Resources)
                        {
                            string computedHash = (RootDirectory / resource.Path).GetFileHash();
                            if (computedHash != resource.Hash)
                            {
                                resource.Hash = computedHash;
                                Log.Information(
                                    "New resource version: {0} -- {1}",
                                    project.ProjectName,
                                    resource.Path
                                );
                                hashChanges = true;
                            }
                        }
                        var nugetVersion = NuGetVersion.Parse(project.PublishedNuGetVersion);
                        var newNugetVersion = new NuGetVersion(
                            nugetVersion.Major,
                            nugetVersion.Minor,
                            nugetVersion.Patch + 1
                        );
                        project.ComputedNuGetVersion = newNugetVersion.ToString();
                    }
                    if (hashChanges)
                    {
                        await SaveJsonAsync(
                            ProjectDependencies,
                            RootDirectory / "resources.lock.json"
                        );
                    }
                });

    Target UnzipCodesCsv =>
        _ =>
            _.DependsOn(UpdateProjectLock)
                .Executes(() =>
                {
                    var tempDir = MakeTempDirectory("zipcodes");
                    (RootDirectory / "resources" / "CPdescargatxt.zip").UnZipTo(tempDir);
                    zipCodesCsvPath = tempDir / "CPdescarga.txt";
                });

    Target CreateZipCodesDb =>
        _ =>
            _.DependsOn(UnzipCodesCsv)
                .Executes(() =>
                {
                    var csv = new Csv(zipCodesCsvPath, "|", 1);
                    var data = csv.Rows().Select(ParseCsvEntryRow);
                    using var db = new LiteDatabase(zipCodesCsvPath.Parent / "data.db");
                    var entriesCollection = db.GetCollection<ZipCodeEntry>("entries");
                    entriesCollection.InsertBulk(data);
                    entriesCollection.EnsureIndex(x => x.ZipCode);
                    entriesCollection.EnsureIndex(x => x.Settlement.Name);
                    entriesCollection.EnsureIndex(x => x.Municipality.Name);
                    entriesCollection.EnsureIndex(x => x.City.Name);
                    entriesCollection.EnsureIndex(x => x.State.Name);
                });

    Target CopyZipCodesDb =>
        _ =>
            _.DependsOn(CreateZipCodesDb)
                .Executes(() =>
                {
                    (
                        RootDirectory / "src" / "MexicoData.ZipCodes" / "Data" / "data.db"
                    ).DeleteFile();
                    (zipCodesCsvPath.Parent / "data.db").MoveToDirectory(
                        RootDirectory / "src" / "MexicoData.ZipCodes" / "Data"
                    );
                });

    Target Compile =>
        _ =>
            _.DependsOn(CopyZipCodesDb)
                .Executes(() =>
                {
                    var buildSettings = new DotNetBuildSettings()
                        .SetProjectFile(
                            RootDirectory
                                / "src"
                                / "MexicoData.ZipCodes"
                                / "MexicoData.ZipCodes.csproj"
                        )
                        .SetConfiguration("Release");
                    DotNetTasks.DotNetBuild(buildSettings);
                    var packSettings = new DotNetPackSettings()
                        .SetProject(
                            RootDirectory
                                / "src"
                                / "MexicoData.ZipCodes"
                                / "MexicoData.ZipCodes.csproj"
                        )
                        .SetConfiguration("Release")
                        .SetOutputDirectory(RootDirectory / "artifacts");
                    DotNetTasks.DotNetPack(packSettings);
                });

    private async Task<T> LoadJsonAsync<T>(AbsolutePath filePath)
    {
        using FileStream openStream = File.OpenRead(filePath);
        return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(openStream);
    }

    private async Task SaveJsonAsync<T>(T obj, AbsolutePath path)
    {
        using FileStream createStream = File.Create(path);
        await System.Text.Json.JsonSerializer.SerializeAsync(
            createStream,
            obj,
            options: new JsonSerializerOptions { WriteIndented = true }
        );
    }

    private AbsolutePath MakeTempDirectory(string prefix = "")
    {
        var dir =
            RootDirectory / "temp" / prefix + "_" + Path.GetRandomFileName().Split(".").First();
        dir.CreateOrCleanDirectory();
        return dir;
    }

    private ZipCodeEntry ParseCsvEntryRow(Dictionary<string, string> row)
    {
        return new ZipCodeEntry
        {
            ZipCode = row["d_codigo"],
            Settlement = new Settlement
            {
                Id = row["id_asenta_cpcons"],
                Name = row["d_asenta"],
                Type = new SettlementType
                {
                    Id = row["c_tipo_asenta"],
                    Name = row["d_tipo_asenta"]
                },
                Zone = row["d_zona"],
            },
            Municipality = new Municipality { Id = row["c_mnpio"], Name = row["D_mnpio"] },
            City = new City { Id = row["c_cve_ciudad"], Name = row["d_ciudad"], },
            State = new State { Id = row["c_estado"], Name = row["d_estado"], }
        };
    }
}
