using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Xunit;

namespace Banking.Api.Tests.Repositories
{
    public class BeneficiaryRepositoryTests
    {
        private readonly InMemoryDb _db;
        private readonly BeneficiaryRepository _repo;

        public BeneficiaryRepositoryTests()
        {
            _db = new InMemoryDb();
            _repo = new BeneficiaryRepository(_db);
        }

        [Fact]
        public void GetBeneficiaries_ShouldReturnOnlyUserBeneficiaries()
        {
            int userId = 1;

            var result = _repo.GetBeneficiaries(userId).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, b => Assert.Equal(userId, b.UserId));
        }

        [Fact]
        public void GetBeneficiaries_ShouldReturnSortedByName()
        {
            int userId = 1;

            var result = _repo.GetBeneficiaries(userId).ToList();

            var sorted = result.OrderBy(b => b.Name).ToList();

            Assert.Equal(sorted, result);
        }

        [Fact]
        public void AddBeneficiary_ShouldAssignIncrementalId()
        {
            var newBeneficiary = new Beneficiary
            {
                UserId = 1,
                Name = "Test User",
                AccountNumber = "ACC123",
                Nickname = "Tester"
            };

            var created = _repo.AddBeneficiary(newBeneficiary);

            Assert.True(created.Id > 0);
            Assert.Contains(_db.Beneficiaries, b => b.Id == created.Id);
        }

        [Fact]
        public void AddBeneficiary_ShouldIncreaseCount()
        {
            int initialCount = _db.Beneficiaries.Count;

            _repo.AddBeneficiary(new Beneficiary
            {
                UserId = 1,
                Name = "New User",
                AccountNumber = "ACC999",
                Nickname = "NewNick"
            });

            Assert.Equal(initialCount + 1, _db.Beneficiaries.Count);
        }

        [Fact]
        public void DeleteBeneficiary_ShouldRemoveBeneficiary()
        {
            int userId = 1;
            var existing = _db.Beneficiaries.First(b => b.UserId == userId);

            _repo.DeleteBeneficiary(userId, existing.Id);

            Assert.DoesNotContain(_db.Beneficiaries, b => b.Id == existing.Id);
        }

        [Fact]
        public void DeleteBeneficiary_ShouldDoNothing_WhenNotFound()
        {
            int initialCount = _db.Beneficiaries.Count;

            _repo.DeleteBeneficiary(1, 9999);

            Assert.Equal(initialCount, _db.Beneficiaries.Count);
        }
    }
}
