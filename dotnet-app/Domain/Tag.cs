namespace Domain;

public record Tag
{
    private Tag() { }
    public Tag(string tagName)
    {
        Id = Guid.NewGuid();
        Name = tagName;
    }

    public Tag(Guid id, string tagName)
    {
        Id = id;
        Name = tagName;
    }

    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public IList<Message> Messages { get; init; } = new List<Message>();
}
