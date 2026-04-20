using Banking.Api.Domain;

namespace Banking.Api.Services;

public interface IAccountService
{
    IEnumerable<Account> GetUserAccounts(int userId);
    Account? GetAccount(int accountId);
    IEnumerable<object> GetCreditAccounts(int userId);

}
