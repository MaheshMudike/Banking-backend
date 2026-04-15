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
        // In-memory DB updates automatically because it's a reference type
    }
}
