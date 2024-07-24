using Microsoft.EntityFrameworkCore;
using UsersManagement.Models;

namespace UsersManagement.Repositories;

public class UserDbContext : DbContext
{
    //Почему не добавили DbSet<>? 
    //public DbSet<User> Users { get; set; }
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected UserDbContext(DbContextOptions options) : base(options)
    {
    }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().ToTable("User").HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();

        modelBuilder.Entity<UserEntity>().Property(x => x.Name).HasColumnName("Name").IsRequired(false);
        modelBuilder.Entity<UserEntity>().Property(x => x.Email).HasColumnName("Email").IsRequired(false);
        modelBuilder.Entity<UserEntity>().Property(x => x.BirthDate).HasColumnName("BirthDate").IsRequired(false);

        base.OnModelCreating(modelBuilder);
    }
}
