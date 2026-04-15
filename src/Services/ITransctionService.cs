using Banking.Api.Domain;

namespace Banking.Api.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GetRecentTransactions(int accountId);
}
