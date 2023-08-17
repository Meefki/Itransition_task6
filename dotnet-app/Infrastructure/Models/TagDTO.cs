namespace Infrastructure.Models;

public class TagDTO
{
    public TagDTO()
    {
        MessageTags = new();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<MessageTagDTO> MessageTags { get; set; }
}
