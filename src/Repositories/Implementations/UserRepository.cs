using Banking.Api.Domain;
using Banking.Api.Infrastructure;

namespace Banking.Api.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly InMemoryDb _db;

    public UserRepository(InMemoryDb db)
    {
        _db = db;
    }

    public User? GetUser(string username, string password)
    {
        return _db.Users.FirstOrDefault(u => 
            u.Username == username && u.Password == password);
    }
}
