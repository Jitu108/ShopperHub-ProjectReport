using AutoMapper;
using Catalog.API.ProtoService;
using DiscountAPI.Dtos;
using DiscountAPI.Entities;
using DiscountAPI.ProtoService;

namespace DiscountAPI.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<GrpcCatalogProduct, Product>()
                .ForMember(dest => dest.ProductExternalId, options => options.MapFrom(src => src.ProductId));
            
            CreateMap<Product, ProductDiscount>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.ProductExternalId));

            CreateMap<GrpcDiscountUpdate, DiscountUpdateDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.IsPercent, options => options.MapFrom(src => src.IsPercent))
                .ForMember(dest => dest.Discount, options => options.MapFrom(src => src.Discount));

            CreateMap<ProductDiscount, GrpcProductDiscount>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.DiscountFlat, options => options.MapFrom(src => src.DiscountFlat))
                .ForMember(dest => dest.DiscountPercent, options => options.MapFrom(src => src.DiscountPercent))
                .ForMember(dest => dest.IsDiscountPercent, options => options.MapFrom(src => src.IsDiscountPercent))
                .ForMember(dest => dest.Mrp, options => options.MapFrom(src => src.MRP))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.Price));

            CreateMap<ProductDiscount, Product>()
                .ForMember(dest => dest.ProductExternalId, options => options.MapFrom(src => src.ProductId));

            CreateMap<GrpcProductDiscount, ProductDiscount>()
                .ForMember(dest => dest.MRP, options => options.MapFrom(src => src.Mrp));

            CreateMap<GrpcCatalogProduct, ProductRead>()
                .ForMember(dest => dest.ProductExternalId, option => option.MapFrom(src => src.ProductId));

            CreateMap<ProductRead, Product>()
                .ForMember(dest => dest.DiscountFlat, options => options.MapFrom(src => (src.MRP > src.Price) && !src.IsDiscountPercent ? src.MRP - src.Price : 0));
        }
    }
}
