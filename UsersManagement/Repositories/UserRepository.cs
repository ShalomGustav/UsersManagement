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

    public async Task<UserEntity[]> GetUsersByIdsAsync(string[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return new UserEntity[] { };
        }

        var query = Users.Where(x => ids.Contains(x.Id));
        var result = await query.ToArrayAsync();
        return result;
    }
}
