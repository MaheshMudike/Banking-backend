using Banking.Api.Domain;

namespace Banking.Api.Repositories;

public interface IAccountRepository
{
    IEnumerable<Account> GetAccountsByUser(int userId);
    Account? GetAccount(int accountId);
    void UpdateAccount(Account account);
}
