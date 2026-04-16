using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;

    public TransactionService(ITransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public IEnumerable<Transaction> GetRecentTransactions(int accountId)
    {
        return _transactionRepo.GetTransactions(accountId);
    }

    public IEnumerable<Transaction> FilterTransactions(int accountId, string? type, DateTime? from, DateTime? to)
    {
        var transactions = _transactionRepo.GetTransactions(accountId).AsQueryable();
        if (!string.IsNullOrWhiteSpace(type)){
            transactions = transactions.Where(t =>
            t.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        }
        if (from.HasValue){
            transactions = transactions.Where(t => t.Date >= from.Value);
        }
        if (to.HasValue){
            transactions = transactions.Where(t => t.Date <= to.Value);
        }
        // You can still limit to last 10 if required:
        return transactions
            .OrderByDescending(t => t.Date)
            .ToList();
    }

}
