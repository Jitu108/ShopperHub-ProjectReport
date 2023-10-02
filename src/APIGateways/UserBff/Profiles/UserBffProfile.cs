using AutoMapper;
using Basket.API.ProtoService;
using Catalog.API.ProtoService;
using DiscountAPI.ProtoService;
using Identity.API.ProtoService;
using Ordering.API.ProtoService;
using UserBff.Dtos;

namespace AdminBff.Profiles
{
    public class UserBffProfile : Profile
    {
        public UserBffProfile()
        {
            CreateMap<GrpcCatalogProductDetailed, ProductRead>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (decimal?)src.Price))
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => (decimal)src.Mrp))
                .ForMember(dest => dest.CatalogTypeId, option => option.MapFrom(src => src.CatalogTypeId))
                .ForMember(dest => dest.CatalogType, option => option.MapFrom(src => src.CatalogType))
                .ForMember(dest => dest.CatalogBrandId, option => option.MapFrom(src => src.CatalogBrandId))
                .ForMember(dest => dest.CatalogBrand, option => option.MapFrom(src => src.CatalogBrand))
                .ForMember(dest => dest.AvailableStock, option => option.MapFrom(src => src.AvailableStock))
                .ForMember(dest => dest.RestockThreshold, option => option.MapFrom(src => src.RestockThreshold))
                .ForMember(dest => dest.MaxStockThreshold, option => option.MapFrom(src => src.MaxStockThreshold))
                .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.ImageCaption))
                .ForMember(dest => dest.ImageData, option => option.MapFrom(src => src.ImageData));

            CreateMap<ProductCreate, GrpcCatalogProductToCreate>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (decimal?)src.Price))
                .ForMember(dest => dest.Mrp, option => option.MapFrom(src => (decimal)src.MRP))
                .ForMember(dest => dest.CatalogTypeId, option => option.MapFrom(src => src.CatalogTypeId))
                .ForMember(dest => dest.CatalogBrandId, option => option.MapFrom(src => src.CatalogBrandId))
                .ForMember(dest => dest.AvailableStock, option => option.MapFrom(src => src.AvailableStock))
                .ForMember(dest => dest.RestockThreshold, option => option.MapFrom(src => src.RestockThreshold))
                .ForMember(dest => dest.MaxStockThreshold, option => option.MapFrom(src => src.MaxStockThreshold))
                .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.ImageCaption))
                .ForMember(dest => dest.ImageData, option => option.MapFrom(src => src.ImageData));

            CreateMap<ProductCreate, GrpcCatalogProductToUpdate>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (decimal?)src.Price))
                .ForMember(dest => dest.Mrp, option => option.MapFrom(src => (decimal)src.MRP))
                .ForMember(dest => dest.CatalogTypeId, option => option.MapFrom(src => src.CatalogTypeId))
                .ForMember(dest => dest.CatalogBrandId, option => option.MapFrom(src => src.CatalogBrandId))
                .ForMember(dest => dest.AvailableStock, option => option.MapFrom(src => src.AvailableStock))
                .ForMember(dest => dest.RestockThreshold, option => option.MapFrom(src => src.RestockThreshold))
                .ForMember(dest => dest.MaxStockThreshold, option => option.MapFrom(src => src.MaxStockThreshold))
                .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.ImageCaption))
                .ForMember(dest => dest.ImageData, option => option.MapFrom(src => src.ImageData));

            CreateMap<CatalogBrandCreate, GrpcCatalogBrandToCreate>();

            CreateMap<CatalogBrandUpdate, GrpcCatalogBrandToUpdate>();

            CreateMap<GrpcCatalogBrand, CatalogBrandRead>();

            CreateMap<CatalogTypeCreate, GrpcCatalogTypeToCreate>();

            CreateMap<CatalogTypeUpdate, GrpcCatalogTypeToUpdate>();

            CreateMap<GrpcCatalogType, CatalogTypeRead>();

            CreateMap<GrpcProductDiscount, ProductDiscount>()
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => src.Mrp));


            CreateMap<DiscountUpdateDto, GrpcDiscountUpdate>();

            CreateMap<ShoppingCartItemCreate, ShoppingCartItemDto>();

            CreateMap<GrpcShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (decimal)src.TotalPrice));

            CreateMap<ShoppingCartDto, GrpcShoppingCart>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (double)src.TotalPrice));

            CreateMap<GrpcShoppingCartItem, ShoppingCartItemDto>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (decimal)src.UnitPrice));

            CreateMap<ShoppingCartItemDto, GrpcShoppingCartItem>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (double)src.UnitPrice));

            // Identity
            CreateMap<UserCreate, GrpcAddUserRequest>();
            CreateMap<GrpcIdentityUser, UserDto>();
            CreateMap<GrpcIdentityAuthenticateResponse, AuthUserDto>()
                .ForMember(dest => dest.ExpiryDate, options => options.MapFrom(src => src.ExpiryDate.ToDateTime()));

            // Order
            CreateMap<OrderCreate, GrpcOrderCreate>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (double)src.TotalPrice));

            CreateMap<OrderItemCreate, GrpcOrderItemCreate>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (double)src.UnitPrice));

            CreateMap<AddressDto, GrpcAddressDto>();

            CreateMap<GrpcOrderDto, OrderDto>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (decimal)src.TotalPrice))
                .ForMember(dest => dest.OrderDate, option => option.MapFrom(src => src.OrderDate.ToDateTime()));

            CreateMap<GrpcOrderItemDto, OrderItemDto>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (decimal)src.UnitPrice));

            CreateMap<GrpcAddressDto, AddressDto>();

            CreateMap<CancelOrderDto, GrpcCancelOrderDto>();

            CreateMap<GrpcRefundedOrderDto, RefundedOrderDto>()
            .ForMember(dest => dest.RefundDate, option => option.MapFrom(src => src.RefundDate.ToDateTime()))
            .ForMember(dest => dest.RefundedAmount, option => option.MapFrom(src => (decimal)src.RefundedAmount));

            CreateMap<GrpcCancelledOrderDto, CancelledOrderDto>()
                .ForMember(dest => dest.CancellationDate, option => option.MapFrom(src => src.CancellationDate.ToDateTime()));

            CreateMap<GrpcRefundedOrderDto, RefundedOrderDto>()
                .ForMember(dest => dest.RefundedAmount, option => option.MapFrom(src => (decimal)src.RefundedAmount))
                .ForMember(dest => dest.RefundDate, option => option.MapFrom(src => src.RefundDate.ToDateTime()));
        }
    }
}
