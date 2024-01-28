namespace _build.Models;

internal record ResourceHash
{
    public string Path { get; init; }

    public string Hash { get; set; }
}
