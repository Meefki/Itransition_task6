using Domain;

namespace Application.UseCases;

public interface IMessageService
{
    Task<IEnumerable<Tag>> WriteMessageAsync(Message message);
}
