using Domain;

namespace Infrastructure.Models;

internal static class Mapper
{
    public static Tag MapDtoToEntity(TagDTO dto)
    {
        return new(
            dto.Id,
            dto.Name);
    }

    public static TagDTO MapEntityToDto(Tag entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static Message MapDtoToEntity(MessageDTO dto)
    {
        return new(
            dto.Id,
            dto.Value,
            dto.MessageTags.Select(x => MapDtoToEntity(x.Tag)).ToHashSet(),
            dto.SentDate);
    }

    public static MessageDTO MapEntityToDto(Message entity)
    {
        return new()
        {
            Id = entity.Id,
            Value = entity.Value,
            SentDate = entity.SentDate,
        };
    }
}
