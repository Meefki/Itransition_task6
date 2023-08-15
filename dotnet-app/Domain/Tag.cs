namespace Domain;

public record Tag
{
    private Tag() { }
    public Tag(int id, string tagName)
    {
        Id = id;
        Name = tagName;
    }

    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public IList<Message> Messages { get; init; } = new List<Message>();
}
