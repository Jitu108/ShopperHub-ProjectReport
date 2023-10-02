using AdminBff.Dtos;
using AutoMapper;
using Grpc.Net.Client;
using Identity.API.ProtoService;
using Polly;

namespace AdminBff.InterServiceCommunication.SyncDataClient
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
