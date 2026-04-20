using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepo;

    public AccountService(IAccountRepository accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public IEnumerable<Account> GetUserAccounts(int userId)
    {
        return _accountRepo.GetAccountsByUser(userId);
    }

    public IEnumerable<object> GetCreditAccounts(int userId)
    {
        return _accountRepo.GetCreditAccounts(userId);
    }


    public Account? GetAccount(int accountId)
    {
        return _accountRepo.GetAccount(accountId);
    }
}
