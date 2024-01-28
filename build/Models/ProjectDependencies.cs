namespace _build.Models;

internal record ProjectDependencies
{
    public string ProjectName { get; init; }

    public string PublishedNuGetVersion { get; init; }

    public string ComputedNuGetVersion { get; set; }

    public List<ResourceHash> Resources { get; init; }
}
