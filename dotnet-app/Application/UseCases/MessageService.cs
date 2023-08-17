using Application.Repositories;
using Domain;

namespace Application.UseCases;

public class MessageService : IMessageService
{
    private readonly ITagRepository tagRepository;
    private readonly IMessageRepository messageRepository;

    public MessageService(
        ITagRepository tagRepository,
        IMessageRepository messageRepository)
    {
        this.tagRepository = tagRepository;
        this.messageRepository = messageRepository;
    }

    public async Task<IEnumerable<Tag>> WriteAsync(Message message)
    {
        ITransaction? transaction = null;
        IEnumerable<Tag> newTags = new List<Tag>();
        IEnumerable<Tag> existingTags = new List<Tag>();

        try
        {
            (newTags, existingTags) = await tagRepository.CheckExistingAsync(message.Tags);
            if (newTags.Any()) 
                transaction = await tagRepository.CreateAsync(newTags);
            transaction = await messageRepository.CreateAsync(message, transaction);
            if (message.Tags.Count > 0)
            {
                var tags = new List<Tag>(newTags);
                tags.AddRange(existingTags);
                transaction = await messageRepository.LinkTagsAsync(message, tags, transaction);
            }

            if (transaction is null) throw new InvalidOperationException("Operation was failed during saving a message/tags");
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            transaction?.RollbackAsync();
            throw;
        }

        return newTags;
    }
}
