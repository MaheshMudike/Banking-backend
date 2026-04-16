using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Banking.Api.Services.Implementations;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly InMemoryDb _db;
        private readonly TransactionRepository _repo;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _db = new InMemoryDb();
            _repo = new TransactionRepository(_db);
            _service = new TransactionService(_repo);
        }

        [Fact]
        public void GetRecentTransactions_ShouldReturnAtMost10()
        {
            // Arrange
            int accountId = 1;

            // Act
            var result = _service.GetRecentTransactions(accountId).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count <= 10);
            Assert.All(result, t => Assert.Equal(accountId, t.AccountId));
        }

        [Fact]
        public void FilterTransactions_ByType_ShouldReturnOnlyMatchingType()
        {
            // Arrange
            int accountId = 1;
            string type = "DEBIT";

            // Act
            var result = _service.FilterTransactions(accountId, type, null, null).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, t => Assert.Equal(type, t.Type));
        }

        [Fact]
        public void FilterTransactions_ByDateRange_ShouldReturnOnlyWithinRange()
        {
            // Arrange
            int accountId = 1;
            var from = DateTime.Now.AddDays(-4);
            var to = DateTime.Now;

            // Act
            var result = _service.FilterTransactions(accountId, null, from, to).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, t => Assert.True(t.Date >= from && t.Date <= to));
        }
    }
}
