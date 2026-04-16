using Banking.Api.Domain;

namespace Banking.Api.Services;

public interface IUserService
{
    User? ValidateUser(string username, string password);
}
