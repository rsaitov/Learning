using BasicAuth.Model;

namespace BasicAuth.Repository;
public class UserRepository : IUserRepository
{
    private List<User> _users = new List<User>
        {
            new User
            {
                Id = 1, Username = "peter", Password = "peter123"
            },
            new User
            {
                Id = 2, Username = "joydip", Password = "joydip123"
            },
            new User
            {
                Id = 3, Username = "james", Password = "james123"
            }
        };

    public async Task<User> Authenticate(string username, string password)
    {
        var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
        if (user != null)
        {
            return await Task.FromResult(user);
        }

        return await Task.FromResult<User>(null);
    }

    public async Task<List<string>> GetUsernames()
    {
        var users = new List<string>();
        foreach (var user in _users)
        {
            users.Add(user.Username);
        }
        return await Task.FromResult(users);
    }
}
