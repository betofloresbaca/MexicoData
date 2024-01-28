namespace _build.Models;

internal record Project
{
    public string ProjectName { get; init; }

    public string PublishedNuGetVersion { get; init; }

    public string ComputedNuGetVersion { get; set; }

    public List<Resource> Resources { get; init; }
}
