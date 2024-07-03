using UsersManagement.Models;

namespace UsersManagement.Repositories;

public interface IUserRepository 
{
    Task<UserEntity[]> GetUserByIdsAsync(string[] ids);
}
