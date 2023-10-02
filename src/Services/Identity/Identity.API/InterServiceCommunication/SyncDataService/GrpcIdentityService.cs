using AutoMapper;
using Grpc.Core;
using Identity.API.Dtos;
using Identity.API.ProtoService;
using Identity.API.Services;

namespace Identity.API.InterServiceCommunication.SyncDataService
{
    public class GrpcIdentityService : GrpcIdentityProvider.GrpcIdentityProviderBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public GrpcIdentityService(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public override async Task<GrpcIdentityBool> GrpcAddUser(GrpcAddUserRequest request, ServerCallContext context)
        {
            var req = mapper.Map<UserCreate>(request);
            var status = await userService.AddUser(req);
            var grpcStatus = new GrpcIdentityBool { Response = status };
            return grpcStatus;
        }

        public override async Task<GrpcIdentityUser> GrpcGetUserByEmail(GrpcUserEmailRequest request, ServerCallContext context)
        {
            var user = await userService.GetUserByEmail(request.Email);

            var grpcUser = mapper.Map<GrpcIdentityUser>(user);
            return grpcUser;
        }

        public async override Task<GrpcIdentityUser> GrpcGetUserById(GrpcUserIdRequest request, ServerCallContext context)
        {
            var user = await userService.GetUserById(request.Id);

            var grpcUser = mapper.Map<GrpcIdentityUser>(user);
            return grpcUser;
        }

        public async override Task<GrpcIdentityAuthenticateResponse> GrpcAuthenticate(GrpcIdentityAuthenticateRequest request, ServerCallContext context)
        {
            try
            {
                var authUser = await userService.Authenticate(request.Email, request.Password);
                var grpcAuthUser = mapper.Map<GrpcIdentityAuthenticateResponse>(authUser);
                return grpcAuthUser;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
