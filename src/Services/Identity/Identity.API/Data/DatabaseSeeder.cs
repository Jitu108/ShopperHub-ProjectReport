using Identity.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data
{
    public class DatabaseSeeder
    {
        public async Task SeedAsync(UserDbContext context)
        {
            await PopulateTables(context);

        }

        private async Task PopulateTables(UserDbContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();

            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(new List<Role> {
                   new Role { Name = "Admin"},
                   new Role { Name = "User"},
                });
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var role = await context.Roles.Where(x => x.Name == "Admin").FirstOrDefaultAsync();
                if (role != null)
                {
                    await context.Users.AddRangeAsync(new List<User> {
                   new User {
                       FirstName = "Jitendra",
                       LastName = "Gupta",
                       Email = "gupta.jitendra108@gmail.com",
                       Password = "jiten",
                       Role = role,
                       CreatedDate = DateTime.Now
                   }
                });
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
