using Domain;

namespace Application.Repositories;

public interface IMessageRepository
{
    Task<ITransaction> CreateAsync(Message message, ITransaction? transaction = null);
    Task<IEnumerable<Message>> GetAsync(IEnumerable<string> tags);
    Task<ITransaction> LinkTagsAsync(Message message, IEnumerable<Tag> tags, ITransaction? transaction = null);
}
