namespace Domain;

public record Tag
{
    public Tag() { }
    public Tag(string tagName)
    {
        Name = tagName;
    }

    public string Name { get; init; } = null!;
}
