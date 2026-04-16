using Banking.Api.Domain;

namespace Banking.Api.Repositories;

public interface IUserRepository
{
    User? GetUser(string username, string password);
}
