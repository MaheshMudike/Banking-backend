using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Banking.Api.Services.Implementations;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class BeneficiaryServiceTests
    {
        private readonly InMemoryDb _db;
        private readonly BeneficiaryRepository _repo;
        private readonly BeneficiaryService _service;

        public BeneficiaryServiceTests()
        {
            _db = new InMemoryDb();
            _repo = new BeneficiaryRepository(_db);
            _service = new BeneficiaryService(_repo);
        }

        [Fact]
        public void GetBeneficiaries_ShouldReturnBeneficiaries_ForUser()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = _service.GetBeneficiaries(userId).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.All(result, b => Assert.Equal(userId, b.UserId));
        }

        [Fact]
        public void AddBeneficiary_ShouldAddNewBeneficiary()
        {
            // Arrange
            int userId = 1;
            string name = "Test User";
            string accountNumber = "ACC9999";
            string nickname = "Tester";

            // Act
            var created = _service.AddBeneficiary(userId, name, accountNumber, nickname);
            var list = _service.GetBeneficiaries(userId).ToList();

            // Assert
            Assert.NotNull(created);
            Assert.Contains(list, b => b.Id == created.Id);
            Assert.Equal(name, created.Name);
            Assert.Equal(accountNumber, created.AccountNumber);
            Assert.Equal(nickname, created.Nickname);
        }

        [Fact]
        public void DeleteBeneficiary_ShouldRemoveBeneficiary()
        {
            // Arrange
            int userId = 1;
            var existing = _service.GetBeneficiaries(userId).First();
            int id = existing.Id;

            // Act
            _service.DeleteBeneficiary(userId, id);
            var list = _service.GetBeneficiaries(userId).ToList();

            // Assert
            Assert.DoesNotContain(list, b => b.Id == id);
        }
    }
}
