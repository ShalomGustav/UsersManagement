using UsersManagement.Models;

namespace UsersManagement.Services;

public class UserService
{
    private readonly List<User> _users;

    public UserService()
    {
        _users = new List<User>();
    }

    public List<User> GetUsers()
    {
        return _users;
    }

    public User GetUserById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }
        
        var user = _users.FirstOrDefault(x => x.Id == id);
        
        if(user == null)
        {
            throw new NullReferenceException(nameof(user));
        }

        return user;
    }
    //
    public void CreateUser(User user)
    {
        if(user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var result = _users.FirstOrDefault(x => x.Id == user.Id || x.Login == user.Login);
        
        if(result != null)
        {
            throw new Exception("Ошибка создания пользователя");
        }

        _users.Add(user);
    }

    public User UpdateUser(User user)
    {
        if(user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if(string.IsNullOrEmpty(user.Id))
        {
            user.Id = Guid.NewGuid().ToString();
        }
        else
        {
            var userById = GetUserById(user.Id);
            _users.Remove(userById);
        }
        _users.Add(user);
        return user;
    }

    public void DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var user = GetUserById(id);
        _users.Remove(user);
    }
}
