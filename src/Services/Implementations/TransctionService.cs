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
}
