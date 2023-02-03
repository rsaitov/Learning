using BasicAuth.Model;

namespace BasicAuth.Repository;

public interface IUserRepository
{
    Task<User> Authenticate(string username, string password);
    Task<List<string>> GetUsernames();
}
