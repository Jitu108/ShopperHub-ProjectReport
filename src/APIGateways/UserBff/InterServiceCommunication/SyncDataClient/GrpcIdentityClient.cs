using AutoMapper;
using Basket.API.ProtoService;
using Grpc.Net.Client;
using Identity.API.ProtoService;
using Polly;
using UserBff.Dtos;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcIdentityClient : IIdentityClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcIdentityClient> logger;

        public GrpcIdentityClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcIdentityClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<bool> AddUser(UserCreate user)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add User to Identity Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:IdentityService"]);
            var client = new GrpcIdentityProvider.GrpcIdentityProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcAddUserRequest>(user);
                    var cartResponse = await client.GrpcAddUserAsync(request);
                    status = cartResponse.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Add User to Identity Service", ex.Message);
            }

            return status;
        }

        public async Task<AuthUserDto> Authenticate(string email, string password)
        {
            AuthUserDto authUser = new AuthUserDto();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Authenticate User with Identity Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:IdentityService"]);
            var client = new GrpcIdentityProvider.GrpcIdentityProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcIdentityAuthenticateRequest { Email = email, Password = password };
                    var grpcUser = await client.GrpcAuthenticateAsync(request);
                    authUser = mapper.Map<AuthUserDto>(grpcUser);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Authenticate User with Identity Service", ex.Message);
            }

            return authUser;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            UserDto user = new UserDto();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get User with Email from Identity Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:IdentityService"]);
            var client = new GrpcIdentityProvider.GrpcIdentityProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcUserEmailRequest { Email = email };
                    var grpcUser = await client.GrpcGetUserByEmailAsync(request);
                    user = mapper.Map<UserDto>(grpcUser);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get User with Email from Identity Service", ex.Message);
            }

            return user;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            UserDto user = new UserDto();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get User with Id from Identity Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:IdentityService"]);
            var client = new GrpcIdentityProvider.GrpcIdentityProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcUserIdRequest { Id = id };
                    var grpcUser = await client.GrpcGetUserByIdAsync(request);
                    user = mapper.Map<UserDto>(grpcUser);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get User with Id from Identity Service", ex.Message);
            }

            return user;
        }
    }
}
