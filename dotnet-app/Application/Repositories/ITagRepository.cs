using Domain;

namespace Application.Repositories;

public interface ITagRepository
{
    Task<ITransaction> CreateAsync(IEnumerable<Tag> tags, ITransaction? transaction = null);
    Task<IEnumerable<Tag>> GetAsync(string startWith);
    Task<IEnumerable<Tag>> CheckExistingAsync(IEnumerable<Tag> tags);
}
