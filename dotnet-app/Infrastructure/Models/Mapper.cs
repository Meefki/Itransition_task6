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
            dto.Value,
            dto.MessageTags.Select(x => MapDtoToEntity(x.Tag)).ToHashSet(),
            dto.SentDate,
            dto.Id);
    }

    public static MessageDTO MapEntityToDto(Message entity)
    {
        MessageDTO dto = new()
        {
            Value = entity.Value,
            SentDate = entity.SentDate,
        };
        if (entity.Id.HasValue)
            dto.Id = entity.Id.Value;

        return dto;
    }
}
