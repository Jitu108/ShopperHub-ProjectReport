using Identity.API.Data.Entities;
using System;

namespace Identity.API.Data
{
    public interface IUserRepo
    {
        Task<bool> ValidateCredentials(string email, string password);
        Task<User> Authenticate(string email, string password);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<bool> AddUser(User user);
    }
}
