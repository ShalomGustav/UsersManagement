using UsersManagement.Models;
using UsersManagement.Repositories.Common;

namespace UsersManagement.Repositories;

public interface IUserRepository : IRepository
{
    Task<UserEntity[]> GetUsersByIdsAsync(string[] ids);

    Task<UserEntity> GetUserByLoginAsync(string login);
}
