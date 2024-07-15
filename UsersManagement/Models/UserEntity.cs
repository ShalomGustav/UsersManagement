using System.ComponentModel.DataAnnotations;
using UsersManagement.Repositories.Common;

namespace UsersManagement.Models;

public class UserEntity : Entity, IDateEntity<UserEntity, User>
{
    //[Required]
    //[StringLength(128)]
    //public string Id { get; set; }

    [Required]
    [StringLength(256)]
    public string Login { get; set; }

    [Required]
    [StringLength(256)]
    public string Password { get; set; }

    [StringLength(256)]
    public string Name { get; set; }

    [StringLength(256)]
    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }
    public bool IsAdmin { get; set; }

    public User ToModel(User user)//преобразование из базы в модель  c#
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        user.Id = Id;
        user.Login = Login;
        user.Password = Password;
        user.Name = Name;
        user.Email = Email;
        user.BirthDate = BirthDate;
        user.IsAdmin = IsAdmin;

        return user;
    }

    public UserEntity FromModel(User user)//преобразование из c# в модель базы данных
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        Id = user.Id;
        Login = user.Login;
        Password = user.Password;
        Name = user.Name;
        Email = user.Email;
        BirthDate = user.BirthDate;
        IsAdmin = user.IsAdmin;

        return this;
    }

    public void Patch(UserEntity target) //обновление
    {
        target.Login = Login;
        target.Password = Password;
        target.Name = Name;
        target.Email = Email;
        target.BirthDate = BirthDate;
        target.IsAdmin = IsAdmin;
    }
}
