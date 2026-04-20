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

    public bool Transfer(int fromAccountId, int? toAccountId, string? toAccountNumber, decimal amount, out string message, out string? reference)
    {
        reference = null;

        // 1. Validate amount
        if (amount <= 0)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

        if (amount > 25000)
        {
            message = "Maximum transfer amount is 25,000 AED.";
            return false;
        }

        // 2. Get sender account
        var from = _accountRepo.GetAccount(fromAccountId);
        if (from == null)
        {
            message = "Invalid sender account.";
            return false;
        }

        // 3. Determine receiver account
        Account? to = null;

        if (toAccountId.HasValue)
        {
            to = _accountRepo.GetAccount(toAccountId.Value);
        }
        else if (!string.IsNullOrEmpty(toAccountNumber))
        {
            to = _accountRepo.GetAccountByNumber(toAccountNumber);
        }

        if (to == null)
        {
            message = "Invalid beneficiary or account.";
            return false;
        }

        // 4. Prevent sending to same account
        if (from.Id == to.Id)
        {
            message = "Cannot transfer to the same account.";
            return false;
        }

        // 5. Check balance
        if (from.Balance < amount)
        {
            message = "Insufficient balance.";
            return false;
        }

        // 6. Generate reference
        reference = "TXN-" + Guid.NewGuid().ToString("N")[..10].ToUpper();

        // 7. Debit sender
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

        // 8. Credit receiver
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
