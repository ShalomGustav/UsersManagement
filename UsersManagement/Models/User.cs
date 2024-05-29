using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models;

public class User
{
    public int Id { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    public string Login { get; set; }
    public string Password { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    public string Name { get; set; }
    [RegularExpression(@"@""^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$")]
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsAdmin { get; set; }
}   
