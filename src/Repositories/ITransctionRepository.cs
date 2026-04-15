using Banking.Api.Domain;

namespace Banking.Api.Repositories;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetTransactions(int accountId);
    void AddTransaction(Transaction transaction);
}
