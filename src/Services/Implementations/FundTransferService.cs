using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class FundTransferService : IFundTransferService
{
    private readonly IAccountRepository _accountRepo;
    private readonly ITransactionRepository _transactionRepo;

    public FundTransferService(
        IAccountRepository accountRepo,
        ITransactionRepository transactionRepo)
    {
        _accountRepo = accountRepo;
        _transactionRepo = transactionRepo;
    }

    public bool Transfer(int fromAccountId, int toAccountId, decimal amount, out string message, out string? reference)
    {
        reference = null;

        // 1. Amount must be > 0
        if (amount <= 0)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

         if (amount == null)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

        // 2. Cannot transfer to same account
        if (fromAccountId == toAccountId)
        {
            message = "Cannot transfer to the same account.";
            return false;
        }

        // 3. Max transfer limit
        if (amount > 25000)
        {
            message = "Maximum transfer amount is 25,000 AED.";
            return false;
        }

        var from = _accountRepo.GetAccount(fromAccountId);
        var to = _accountRepo.GetAccount(toAccountId);

        if (from == null || to == null)
        {
            message = "Invalid account.";
            return false;
        }

        // 4. Amount must be <= available balance
        if (from.Balance < amount)
        {
            message = "Insufficient balance.";
            return false;
        }

        // 5. Generate a single reference number for both transactions
        reference = GenerateReference();

        // 6. Debit sender
        from.Balance -= amount;
        _transactionRepo.AddTransaction(new Transaction
        {
            AccountId = from.Id,
            Amount = amount,
            Type = "DEBIT",
            Date = DateTime.Now,
            ReferenceNumber = reference,
            Description = $"Transfer to {to.AccountNumber}"
        });

        // 7. Credit receiver
        to.Balance += amount;
        _transactionRepo.AddTransaction(new Transaction
        {
            AccountId = to.Id,
            Amount = amount,
            Type = "CREDIT",
            Date = DateTime.Now,
            ReferenceNumber = reference,
            Description = $"Transfer from {from.AccountNumber}"
        });

        message = "Transfer successful.";
        return true;
    }

    private string GenerateReference()
    {
        return "TXN-" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
    }
}
