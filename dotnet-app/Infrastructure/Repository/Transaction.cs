using Application.Repositories;
using System.Data.Common;

namespace Infrastructure.Repository;

public class Transaction : ITransaction
{
    private readonly DbTransaction transaction;

    public Transaction(DbTransaction transaction)
    {
        this.transaction = transaction;
    }

    public object Value => transaction;

    public async Task CommitAsync()
    {
        await ((DbTransaction)Value).CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await ((DbTransaction)Value).RollbackAsync();
    }
}
