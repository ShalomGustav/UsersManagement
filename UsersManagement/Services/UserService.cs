﻿using System.Net.WebSockets;
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

        var user = await GetUserByLoginAsync(login);

        if (user == null)
        {
            throw new NullReferenceException(nameof(user));
        }

        return user;    
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

    public void CreateUser(User user)////сделать SaveChangesAsync
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

    public async Task<User> UpdateUser(User user)
    {
        if(user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        //if(string.IsNullOrEmpty(user.Id))
        //{
        //    user.Id = Guid.NewGuid().ToString();
        //}
        //else
        //{
        //    var userById = await GetUserById(user.Id); ///
        //    _users.Remove(userById);
        //}
        //_users.Add(user);
        //return user;

        await SaveChangesAsync(new[] { user });
        return user;
    }

    public async Task DeleteUser(string id)////
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        //var user = await GetUserById(id);////
        //_users.Remove(user);

        await DeleteAsync(new[] {id});
    }

    protected async override Task<UserEntity[]> LoadEntities(
        IRepository repository,
        IEnumerable<string> ids)
    {
       return await ((IUserRepository) repository).GetUsersByIdsAsync(ids.ToArray());
    }
}