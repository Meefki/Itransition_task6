using Application.Network;
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

    public async Task<IEnumerable<Tag>> WriteMessageAsync(Message message)
    {
        ITransaction? transaction = null;
        IEnumerable<Tag> newTags = new List<Tag>();

        try
        {
            newTags = await tagRepository.CheckExistingAsync(message.Tags);
            if (newTags.Any()) transaction = await tagRepository.CreateAsync(newTags);
            await messageRepository.CreateAsync(message, transaction);

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
