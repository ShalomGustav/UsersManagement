using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Models;
using UsersManagement.Services;

namespace UsersManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly User _user;
        private readonly List<User> _users;
        private readonly UserService _userService;

        public IActionResult Index()
        {
            return View();
        }

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet("users")]
        [Authorize]
        public ActionResult<List<User>> GetsUsers(bool admin) 
        {
            if (!admin)
            {
                return new UnauthorizedResult();
            }
            return Ok(_users);
        }

        [HttpPost("users")]
        public async Task SaveChangesAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _userService.SaveChangesAsync(user);
        }

        //[HttpPut("users")]
        //public async Task UpdateUser(User user) 
        //{ 
        //    if (user == null)
        //    {
        //        throw new ArgumentException(nameof(user));
        //    }
        //    await _userService.SaveChangesAsync(user);
        //}

        [HttpDelete("users/{id}")]  
        public void DeleteUser(string id, bool admin)
        { 
            if(string.IsNullOrEmpty(id))
            {
                throw new  ArgumentException("Ошибка удаления пользователя");
            }

            if(admin)
            {
                _userService.DeleteUser(id);
            }
        }
    }
}
