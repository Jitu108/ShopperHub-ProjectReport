using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.ProtoService;
using Catalog.API.Services;
using Grpc.Core;

namespace Catalog.API.InterServiceCommunication.SyncDataService
{
    public class GrpcCatalogBrandService : GrpcCatalogBrandProvider.GrpcCatalogBrandProviderBase
    {
        private readonly ICatalogBrandService brandService;
        private readonly IMapper mapper;

        public GrpcCatalogBrandService(ICatalogBrandService brandService, IMapper mapper)
        {
            this.brandService = brandService;
            this.mapper = mapper;
        }

        public override async Task<GrpcBrandBool> GrpcAddCatalogBrand(GrpcCatalogBrandToCreate request, ServerCallContext context)
        {
            var brand = mapper.Map<CatalogBrandCreate>(request);
            var status = await brandService.AddCatalogBrandAsync(brand);
            var response = new GrpcBrandBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcBrandBool> GrpcUpdateCatalogBrand(GrpcCatalogBrandToUpdate request, ServerCallContext context)
        {
            var brand = mapper.Map<CatalogBrandUpdate>(request);
            var status = await brandService.UpdateCatalogBrandAsync(brand);
            var response = new GrpcBrandBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcBrandBool> GrpcDeleteCatalogBrand(GrpcBrandIdRequest request, ServerCallContext context)
        {
            var status = await brandService.DeleteCatalogBrandAsync(request.Id);
            var response = new GrpcBrandBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcCatalogBrand> GrpcGetCatalogBrandById(GrpcBrandIdRequest request, ServerCallContext context)
        {
            var brand = await brandService.GetCatalogBrandByIdAsync(request.Id);
            var response = mapper.Map<GrpcCatalogBrand>(brand);
            return response;
        }

        public override async Task<GrpcCatalogBrandList> GrpcGetCatalogBrands(GrpcBrandEmptyRequest request, ServerCallContext context)
        {
            var brands = await brandService.GetCatalogBrandsAsync();
            var brandList = mapper.Map<List<GrpcCatalogBrand>>(brands);
            var response = new GrpcCatalogBrandList();
            brandList.ForEach(x => response.Brands.Add(x));
            return response;
        }
    }
}
