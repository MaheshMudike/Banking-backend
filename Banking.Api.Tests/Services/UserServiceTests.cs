using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Banking.Api.Services.Implementations;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class UserServiceTests
    {
        private readonly InMemoryDb _db;
        private readonly UserRepository _repo;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _db = new InMemoryDb();
            _repo = new UserRepository(_db);
            _service = new UserService(_repo);
        }

        [Fact]
        public void ValidateUser_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            var existing = _db.Users.First();

            var result = _service.ValidateUser(existing.Username, existing.Password);

            Assert.NotNull(result);
            Assert.Equal(existing.Username, result.Username);
        }

        [Fact]
        public void ValidateUser_ShouldReturnNull_WhenCredentialsAreWrong()
        {
            var result = _service.ValidateUser("wrongUser", "wrongPass");

            Assert.Null(result);
        }
    }
}
