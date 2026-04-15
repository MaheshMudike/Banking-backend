using Banking.Api.Domain;
using Banking.Api.Repositories;

namespace Banking.Api.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public User? ValidateUser(string username, string password)
    {
        return _userRepo.GetUser(username, password);
    }
}
