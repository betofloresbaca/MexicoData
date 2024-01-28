namespace _build.Models;

internal record Resource
{
    public string Path { get; init; }

    public string Hash { get; set; }
}
