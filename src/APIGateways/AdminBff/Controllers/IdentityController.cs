using AdminBff.Dtos;
using AdminBff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminBff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        
        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await identityService.Authenticate(loginDto.Email, loginDto.Password);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("ByEmail/{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await identityService.GetUserByEmail(email);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("ById/{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await identityService.GetUserById(id);
            return Ok(user);
        }
    }
}
