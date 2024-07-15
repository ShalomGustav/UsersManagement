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
        [Authorize]//
        public ActionResult GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = _userService.GetUserById(id);
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
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userService.CreateUser(user);
        }

        [HttpPut("users")]
        public void UpdateUser(User user) 
        { 
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }
            _userService.UpdateUser(user);
        }

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
