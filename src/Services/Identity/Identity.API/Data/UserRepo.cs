using Identity.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.API.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly UserDbContext context;

        public UserRepo(UserDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddUser(User user)
        {
            var role = await context.Roles.Where(x => x.Name == "User").FirstOrDefaultAsync();

            user.CreatedDate = DateTime.Now;
            user.Role = role;

            await context.Users.AddAsync(user);
            var status = await context.SaveChangesAsync();

            return status > 0;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var hasUser = await context.Users.AnyAsync(x => x.Email == email && x.Password == password);

            return hasUser;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await context.Users.Include(x => x.Role).Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return user;
        }
    }
}
