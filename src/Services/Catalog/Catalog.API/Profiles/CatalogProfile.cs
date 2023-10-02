using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.ProtoService;
using DiscountAPI.ProtoService;
using System.Text;

namespace Catalog.API.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CatalogBrandCreate, CatalogBrand>();
            CreateMap<CatalogBrand, CatalogBrandRead>();


            CreateMap<CatalogBrandUpdate, CatalogBrand>();

            CreateMap<CatalogTypeCreate, CatalogType>();
            CreateMap<CatalogType, CatalogTypeRead>();
            CreateMap<CatalogTypeUpdate, CatalogType>();

            CreateMap<ProductCreate, Product>();
            CreateMap<ProductCreate, Image>()
            .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.ImageId))
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.ImageName))
            .ForMember(dest => dest.Caption, option => option.MapFrom(src => src.ImageCaption))
            .ForMember(dest => dest.Data, option => option.MapFrom(src => src.ImageData != null ? Encoding.ASCII.GetBytes(src.ImageData) : Array.Empty<byte>()));

            CreateMap<Product, ProductRead>()
            .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.Image.Id))
            .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.Image.Name))
            .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.Image.Caption))
            .ForMember(dest => dest.ImageData, option => option.MapFrom(src => Encoding.UTF8.GetString(src.Image.Data)))
            .ForMember(dest => dest.CatalogBrand, option => option.MapFrom(src => src.CatalogBrand.Brand))
            .ForMember(dest => dest.CatalogType, option => option.MapFrom(src => src.CatalogType.Type));

            // GRPC Mappings
            CreateMap<ProductRead, GrpcCatalogProduct>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Id));

            CreateMap<ProductRead, GrpcCatalogProductDetailed>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (double?)src.Price))
                .ForMember(dest => dest.Mrp, option => option.MapFrom(src => (double)src.MRP))
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

            CreateMap<GrpcCatalogProductToCreate, ProductCreate>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (double?)src.Price))
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => (double)src.Mrp))
                .ForMember(dest => dest.CatalogTypeId, option => option.MapFrom(src => src.CatalogTypeId))
                .ForMember(dest => dest.CatalogBrandId, option => option.MapFrom(src => src.CatalogBrandId))
                .ForMember(dest => dest.AvailableStock, option => option.MapFrom(src => src.AvailableStock))
                .ForMember(dest => dest.RestockThreshold, option => option.MapFrom(src => src.RestockThreshold))
                .ForMember(dest => dest.MaxStockThreshold, option => option.MapFrom(src => src.MaxStockThreshold))
                .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.ImageCaption))
                .ForMember(dest => dest.ImageData, option => option.MapFrom(src => src.ImageData));

            CreateMap<GrpcCatalogProductToUpdate, ProductCreate>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => (double?)src.Price))
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => (double)src.Mrp))
                .ForMember(dest => dest.CatalogTypeId, option => option.MapFrom(src => src.CatalogTypeId))
                .ForMember(dest => dest.CatalogBrandId, option => option.MapFrom(src => src.CatalogBrandId))
                .ForMember(dest => dest.AvailableStock, option => option.MapFrom(src => src.AvailableStock))
                .ForMember(dest => dest.RestockThreshold, option => option.MapFrom(src => src.RestockThreshold))
                .ForMember(dest => dest.MaxStockThreshold, option => option.MapFrom(src => src.MaxStockThreshold))
                .ForMember(dest => dest.ImageId, option => option.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageCaption, option => option.MapFrom(src => src.ImageCaption))
                .ForMember(dest => dest.ImageData, option => option.MapFrom(src => src.ImageData));

            CreateMap<GrpcCatalogBrandToCreate, CatalogBrandCreate>()
                .ForMember(dest => dest.Brand, option => option.MapFrom(src => src.Brand));

            CreateMap<GrpcCatalogBrandToUpdate, CatalogBrandUpdate>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Brand, option => option.MapFrom(src => src.Brand));

            CreateMap<CatalogBrandRead, GrpcCatalogBrand>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Brand, option => option.MapFrom(src => src.Brand));

            CreateMap<GrpcCatalogTypeToCreate, CatalogTypeCreate>();
            CreateMap<GrpcCatalogTypeToUpdate, CatalogTypeUpdate>();
            CreateMap<CatalogTypeRead, GrpcCatalogType>();

            CreateMap<GrpcProductDiscount, ProductDiscount>()
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => src.Mrp));

            CreateMap<ProductDiscount, GrpcProductDiscount>()
                .ForMember(dest => dest.Mrp, option => option.MapFrom(src => src.MRP));

            CreateMap<Product, ProductDiscount>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
                .ForMember(dest => dest.DiscountFlat, option => option.MapFrom(src => src.MRP - src.Price))
                .ForMember(dest => dest.DiscountPercent, option => option.MapFrom(src => 0))
                .ForMember(dest => dest.IsDiscountPercent, option => option.MapFrom(src => false))
                .ForMember(dest => dest.MRP, option => option.MapFrom(src => src.MRP))
                .ForMember(dest => dest.Price, option => option.MapFrom(src => src.Price));

        }
    }
}
