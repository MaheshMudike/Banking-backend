using Banking.Api.Domain;
using Banking.Api.Infrastructure;
using Banking.Api.Repositories.Implementations;
using Xunit;

namespace Banking.Api.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly InMemoryDb _db;
        private readonly UserRepository _repo;

        public UserRepositoryTests()
        {
            _db = new InMemoryDb();
            _repo = new UserRepository(_db);
        }

        [Fact]
        public void GetUser_ShouldReturnUser_WhenCredentialsMatch()
        {
            var existing = _db.Users.First();

            var result = _repo.GetUser(existing.Username, existing.Password);

            Assert.NotNull(result);
            Assert.Equal(existing.Username, result!.Username);
            Assert.Equal(existing.Password, result.Password);
        }

        [Fact]
        public void GetUser_ShouldReturnNull_WhenUsernameIsWrong()
        {
            var existing = _db.Users.First();

            var result = _repo.GetUser("wrongUsername", existing.Password);

            Assert.Null(result);
        }

        [Fact]
        public void GetUser_ShouldReturnNull_WhenPasswordIsWrong()
        {
            var existing = _db.Users.First();

            var result = _repo.GetUser(existing.Username, "wrongPassword");

            Assert.Null(result);
        }

        [Fact]
        public void GetUser_ShouldReturnNull_WhenBothAreWrong()
        {
            var result = _repo.GetUser("wrong", "wrong");

            Assert.Null(result);
        }
    }
}
