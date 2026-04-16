using Banking.Api.Domain;

namespace Banking.Api.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GetRecentTransactions(int accountId);
     IEnumerable<Transaction> FilterTransactions(int accountId, string? type, DateTime? from, DateTime? to);
}
