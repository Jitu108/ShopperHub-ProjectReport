using AdminBff.Dtos;
using AutoMapper;
using Catalog.API.ProtoService;
using DiscountAPI.ProtoService;
using Identity.API.ProtoService;

namespace AdminBff.Profiles
{
    public class AdminBffProfile : Profile
    {
        public AdminBffProfile()
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

            // Identity
            CreateMap<GrpcIdentityUser, UserDto>();
            CreateMap<GrpcIdentityAuthenticateResponse, AuthUserDto>()
                .ForMember(dest => dest.ExpiryDate, options => options.MapFrom(src => src.ExpiryDate.ToDateTime()));

        }
    }
}
