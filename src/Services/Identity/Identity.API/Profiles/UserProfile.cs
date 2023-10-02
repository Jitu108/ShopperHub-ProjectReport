using AutoMapper;
using Identity.API.Data.Entities;
using Identity.API.Dtos;
using Identity.API.ProtoService;

namespace Identity.API.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreate, User>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, options => options.MapFrom(src => src.Role.Name));

            CreateMap<User, AuthUserDto>()
                .ForMember(dest => dest.Role, options => options.MapFrom(src => src.Role.Name));

            // Grpc Mappings
            CreateMap<GrpcAddUserRequest, UserCreate>();

            CreateMap<UserDto, GrpcIdentityUser>();

            CreateMap<AuthUserDto, GrpcIdentityAuthenticateResponse>()
                .ForMember(dest => dest.ExpiryDate, options => options.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ExpiryDate)));
        }
    }
}
