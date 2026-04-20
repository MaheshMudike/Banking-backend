using Banking.Api.Domain;
using Banking.Api.Infrastructure;

namespace Banking.Api.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly InMemoryDb _db;

    public AccountRepository(InMemoryDb db)
    {
        _db = db;
    }

    public IEnumerable<Account> GetAccountsByUser(int userId)
    {
        return _db.Accounts.Where(a => a.UserId == userId);
    }

    public Account? GetAccount(int accountId)
    {
        return _db.Accounts.FirstOrDefault(a => a.Id == accountId);
    }

    public void UpdateAccount(Account account)
    {
        // In-memory DB updates will happen automatically
    }

    public IEnumerable<Account> GetUserAccounts(int userId)
    {
        return _db.Accounts.Where(a => a.UserId == userId);
    }

   public IEnumerable<object> GetCreditAccounts(int userId)
    {
        var userAccounts = _db.Accounts
            .Where(a => a.UserId == userId)
            .Select(a => new {
                Id = a.Id,
                Name = a.AccountNumber,
                AccountNumber = a.AccountNumber,
                Type = "ACCOUNT"
            });

        var beneficiaries = _db.Beneficiaries
            .Where(b => b.UserId == userId)
            .Select(b => new {
                Id = b.Id,
                Name = $"{b.Name} ({b.AccountNumber})",
                AccountNumber = b.AccountNumber, 
                Type = "BENEFICIARY"
            });

        return userAccounts.Concat(beneficiaries);
    }

    public void AddExternalAccount(string accountNumber)
    {
        var newId = _db.Accounts.Any()
            ? _db.Accounts.Max(a => a.Id) + 1
            : 1;

        _db.Accounts.Add(new Account
        {
            Id = newId,
            UserId = 0, // external account
            AccountNumber = accountNumber,
            Balance = 0
        });
    }


    public Account? GetAccountByNumber(string accountNumber)
    {
        return _db.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }


}
