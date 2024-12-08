using UsersManagement.Models;
using UsersManagement.Repositories;
using UsersManagement.Repositories.Common;

namespace UsersManagement.Services;

public class UserService : CrudService<User,UserEntity>
{
    private readonly List<User> _users;
    private readonly Func<IUserRepository> _repositoryFactory;

    public UserService(Func<IUserRepository> repositoryFactory) : base(repositoryFactory)
    {
        _users = new List<User>();
        _repositoryFactory = repositoryFactory;
    }

    public List<User> GetUsers()
    {
        return _users;
    }

    public async Task<User> GetByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new ArgumentNullException(nameof(login));
        }

        using (var repository = _repositoryFactory())
        {
            var userEntity = await repository.GetUserByLoginAsync(login);

            if (userEntity == null)
            {
                throw new NullReferenceException(nameof(userEntity));
            }

            var user = userEntity.ToModel(AbstractTypeFactory<User>.TryCreateInstance());
            return user;
        }
    }

    public async Task<User> GetUserById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }
        
        var user = await GetByIdAsync(id);
        
        if(user == null)
        {
            throw new NullReferenceException(nameof(user));
        }

        return user;
    }

    public async Task SaveChangesAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await SaveChangesAsync(new[] { user });
    }

    public async Task DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        await DeleteAsync(new[] {id});
    }

    protected async override Task<UserEntity[]> LoadEntities(
        IRepository repository,
        IEnumerable<string> ids)
    {
       return await ((IUserRepository) repository).GetUsersByIdsAsync(ids.ToArray());
    }
}
