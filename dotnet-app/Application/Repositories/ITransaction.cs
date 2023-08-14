namespace Application.Repositories;

public interface ITransaction
{
    public object Value { get; }
    Task CommitAsync();
    Task RollbackAsync();
}
