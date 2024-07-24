using Microsoft.EntityFrameworkCore;
using UsersManagement.Models;
using UsersManagement.Repositories.Common;

namespace UsersManagement.Repositories;

public class UserRepository : DbContextRepositoryBase<UserDbContext>, IUserRepository
{
    public UserRepository(UserDbContext dbContext, IUnitOfWork unitOfWork = null) : base(dbContext, unitOfWork)
    {
    }

    public IQueryable<UserEntity> Users => DbContext.Set<UserEntity>();

    public async Task<UserEntity[]> GetUsersByIdsAsync(string[] ids)///добавить 
        ///GetUserByLoginAsync добавить в репозитори
        ///
    {
        if (ids == null || ids.Length == 0)
        {
            return new UserEntity[] { };
        }

        var query = Users.Where(x => ids.Contains(x.Id));
        var result = await query.ToArrayAsync();
        return result;
    }
    //
    public async Task<UserEntity> GetUserByLoginAsync(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            return new UserEntity { };
        }

        var query = Users.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
        var result = await query;
        
        return result;
    }
}
