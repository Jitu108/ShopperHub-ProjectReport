using AutoMapper;
using Basket.API.Data.Entities;
using Basket.API.Dtos;
using Basket.API.ProtoService;

namespace Basket.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<ShoppingCartItemCreate, ShoppingCartItem>();

            CreateMap<ShoppingCartItem, GrpcShoppingCartItem>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (decimal)src.UnitPrice));

            CreateMap<GrpcShoppingCartItem, ShoppingCartItem>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (double)src.UnitPrice));

            CreateMap<ShoppingCart, GrpcShoppingCart>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (double)src.TotalPrice));

            CreateMap<GrpcShoppingCart, ShoppingCart>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (decimal)src.TotalPrice));
        }
    }
}