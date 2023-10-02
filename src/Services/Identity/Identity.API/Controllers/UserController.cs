using Identity.API.Dtos;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register(UserCreate user)
        {
            var status = await userService.AddUser(user);
            return Ok(status);
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await userService.Authenticate(email, password);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await userService.GetUserByEmail(email);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserById(id);
            return Ok(user);
        }
    }
}
