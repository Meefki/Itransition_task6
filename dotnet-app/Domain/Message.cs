namespace Domain;

public record Message
{
    private Message() { }
    public Message(int id, string value, ISet<Tag> tags, DateTime? sentDate = null)
    {
        Id = id;
        _tags = tags;
        Value = value;
        SentDate = sentDate ?? DateTime.UtcNow;
    }

    public int Id { get; init; }
    public string Value { get; init; } = null!;
    public DateTime SentDate { get; init; }

    private readonly ISet<Tag> _tags = new HashSet<Tag>();
    public IReadOnlySet<Tag> Tags => (IReadOnlySet<Tag>)_tags;
}
