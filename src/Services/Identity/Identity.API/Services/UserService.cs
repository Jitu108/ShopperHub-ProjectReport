using AutoMapper;
using Identity.API.Data;
using Identity.API.Data.Entities;
using Identity.API.Dtos;
using Identity.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;
        private readonly IMapper mapper;
        private readonly IOptions<AppSettings> appSetings;

        public UserService(IUserRepo repo, IMapper mapper, IOptions<AppSettings> appSetings)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.appSetings = appSetings;
        }
        public async Task<bool> AddUser(UserCreate user)
        {
            var userModel = mapper.Map<User>(user);
            var status = await repo.AddUser(userModel);
            return status;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await repo.GetUserByEmail(email);
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await repo.GetUserById(id);
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<AuthUserDto> Authenticate(string email, string password)
        {
            var key = Encoding.ASCII.GetBytes(appSetings.Value.Secret);
            var userModel = await repo.Authenticate(email, password);
            var user = mapper.Map<AuthUserDto>(userModel);

            if (user == null) return null;

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            var roleClaims = new Claim(ClaimTypes.Role, userModel.Role.Name);

            claims.Add(roleClaims);

            var expiryDate = DateTime.UtcNow.AddHours(24);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    claims.ToArray()
                    ),
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.ExpiryDate = expiryDate;
            return user;
        }
    }
}
