using Domain;

namespace Application.UseCases;

public interface IMessageService
{
    Task<IEnumerable<Tag>> WriteAsync(Message message);
}
