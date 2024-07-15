using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UsersManagement.Repositories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<UserDbContext>();
        builder.UseSqlServer("Data Source=(local);" +
            "Initial Catalog=UserManagement;Persist Security Info=True;" +
            "User ID=test;Password=test;MultipleActiveResultSets=True;" +
            "Connect Timeout=30;TrustServerCertificate=True");
        
        return new UserDbContext(builder.Options);
    }
}
