using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Xunit;

namespace Banking.Api.Tests.Repositories
{
    public class AccountRepositoryTests
    {
        private readonly InMemoryDb _db;
        private readonly AccountRepository _repo;

        public AccountRepositoryTests()
        {
            _db = new InMemoryDb();
            _repo = new AccountRepository(_db);
        }

        [Fact]
        public void GetAccountsByUser_ShouldReturnOnlyUserAccounts()
        {
            int userId = 1;

            var result = _repo.GetAccountsByUser(userId).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, a => Assert.Equal(userId, a.UserId));
        }

        [Fact]
        public void GetAccountsByUser_ShouldReturnEmpty_WhenUserHasNoAccounts()
        {
            int userId = 999;

            var result = _repo.GetAccountsByUser(userId).ToList();

            Assert.Empty(result);
        }

        [Fact]
        public void GetAccount_ShouldReturnCorrectAccount()
        {
            var existing = _db.Accounts.First();

            var result = _repo.GetAccount(existing.Id);

            Assert.NotNull(result);
            Assert.Equal(existing.Id, result!.Id);
        }

        [Fact]
        public void GetAccount_ShouldReturnNull_WhenNotFound()
        {
            var result = _repo.GetAccount(9999);

            Assert.Null(result);
        }

        [Fact]
        public void UpdateAccount_ShouldModifyAccountInMemory()
        {
            var account = _db.Accounts.First();
            decimal oldBalance = account.Balance;

            account.Balance += 500;
            _repo.UpdateAccount(account);

            var updated = _repo.GetAccount(account.Id);

            Assert.NotNull(updated);
            Assert.Equal(oldBalance + 500, updated!.Balance);
        }
    }
}
