using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Banking.Api.Services.Implementations;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly InMemoryDb _db;
        private readonly AccountRepository _repo;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _db = new InMemoryDb();
            _repo = new AccountRepository(_db);
            _service = new AccountService(_repo);
        }

        [Fact]
        public void GetUserAccounts_ShouldReturnAccountsForUser()
        {
            int userId = 1;

            var result = _service.GetUserAccounts(userId).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, a => Assert.Equal(userId, a.UserId));
        }

        [Fact]
        public void GetAccount_ShouldReturnCorrectAccount()
        {
            var account = _db.Accounts.First();

            var result = _service.GetAccount(account.Id);

            Assert.NotNull(result);
            Assert.Equal(account.Id, result.Id);
        }

        [Fact]
        public void GetAccount_ShouldReturnNull_WhenNotFound()
        {
            var result = _service.GetAccount(9999);

            Assert.Null(result);
        }
    }
}
