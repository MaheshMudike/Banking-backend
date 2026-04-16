using Banking.Api.Domain;
using Banking.Api.Infrastructure;

namespace Banking.Api.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly InMemoryDb _db;

    public TransactionRepository(InMemoryDb db)
    {
        _db = db;
    }

    public IEnumerable<Transaction> GetTransactions(int accountId)
    {
        return _db.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Date)
            .Take(10);
    }

    public void AddTransaction(Transaction transaction)
    {
        transaction.Id = _db.Transactions.Any()
            ? _db.Transactions.Max(t => t.Id) + 1
            : 1;

        _db.Transactions.Add(transaction);
    }

    public IEnumerable<Transaction> GetAllTransactionsForAccount(int accountId)
    {
        return _db.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Date);
    }


}
