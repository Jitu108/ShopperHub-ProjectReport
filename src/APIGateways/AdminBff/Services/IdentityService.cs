using AdminBff.Dtos;
using AdminBff.InterServiceCommunication.SyncDataClient;

namespace AdminBff.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityClient identityClient;

        public IdentityService(IIdentityClient identityClient)
        {
            this.identityClient = identityClient;
        }
       
        public Task<AuthUserDto> Authenticate(string email, string password)
        {
            return identityClient.Authenticate(email, password);
        }

        public Task<UserDto> GetUserByEmail(string email)
        {
            return identityClient.GetUserByEmail(email);
        }

        public Task<UserDto> GetUserById(int id)
        {
            return identityClient.GetUserById(id);
        }
    }
}
