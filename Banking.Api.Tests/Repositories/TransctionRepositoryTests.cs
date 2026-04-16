using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Xunit;

namespace Banking.Api.Tests.Repositories
{
    public class TransactionRepositoryTests
    {
        private readonly InMemoryDb _db;
        private readonly TransactionRepository _repo;

        public TransactionRepositoryTests()
        {
            _db = new InMemoryDb();
            _repo = new TransactionRepository(_db);
        }

        [Fact]
        public void GetTransactions_ShouldReturnOnlyAccountTransactions()
        {
            int accountId = 1;

            var result = _repo.GetTransactions(accountId).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, t => Assert.Equal(accountId, t.AccountId));
        }

        [Fact]
        public void GetTransactions_ShouldReturnMax10()
        {
            int accountId = 1;

            var result = _repo.GetTransactions(accountId).ToList();

            Assert.True(result.Count <= 10);
        }

        [Fact]
        public void GetTransactions_ShouldBeSortedByDateDescending()
        {
            int accountId = 1;

            var result = _repo.GetTransactions(accountId).ToList();

            var sorted = result.OrderByDescending(t => t.Date).ToList();

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void AddTransaction_ShouldAssignIncrementalId()
        {
            var newTransaction = new Transaction
            {
                AccountId = 1,
                Amount = 100,
                Type = "DEBIT",
                Date = DateTime.Now
            };

            _repo.AddTransaction(newTransaction);

            Assert.True(newTransaction.Id > 0);
            Assert.Contains(_db.Transactions, t => t.Id == newTransaction.Id);
        }

        [Fact]
        public void AddTransaction_ShouldIncreaseCount()
        {
            int initialCount = _db.Transactions.Count;

            _repo.AddTransaction(new Transaction
            {
                AccountId = 1,
                Amount = 50,
                Type = "CREDIT",
                Date = DateTime.Now
            });

            Assert.Equal(initialCount + 1, _db.Transactions.Count);
        }

        [Fact]
        public void GetAllTransactionsForAccount_ShouldReturnAllSorted()
        {
            int accountId = 1;

            var result = _repo.GetAllTransactionsForAccount(accountId).ToList();
            var sorted = result.OrderByDescending(t => t.Date).ToList();

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void GetAllTransactionsForAccount_ShouldReturnEmpty_WhenNoneExist()
        {
            int accountId = 999;

            var result = _repo.GetAllTransactionsForAccount(accountId).ToList();

            Assert.Empty(result);
        }
    }
}
