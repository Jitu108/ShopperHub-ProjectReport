using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.ProtoService;
using Catalog.API.Services;
using Grpc.Core;

namespace Catalog.API.InterServiceCommunication.SyncDataService
{
    public class GrpcCatalogTypeService : GrpcCatalogTypeProvider.GrpcCatalogTypeProviderBase
    {
        private readonly ICatalogTypeService typeService;
        private readonly IMapper mapper;

        public GrpcCatalogTypeService(ICatalogTypeService typeService, IMapper mapper)
        {
            this.typeService = typeService;
            this.mapper = mapper;
        }

        public override async Task<GrpcTypeBool> GrpcAddCatalogType(GrpcCatalogTypeToCreate request, ServerCallContext context)
        {
            var type = mapper.Map<CatalogTypeCreate>(request);
            var status = await typeService.AddCatalogTypeAsync(type);
            var response = new GrpcTypeBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcTypeBool> GrpcUpdateCatalogType(GrpcCatalogTypeToUpdate request, ServerCallContext context)
        {
            var type = mapper.Map<CatalogTypeUpdate>(request);
            var status = await typeService.UpdateCatalogTypeAsync(type);
            var response = new GrpcTypeBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcTypeBool> GrpcDeleteCatalogType(GrpcTypeIdRequest request, ServerCallContext context)
        {
            var status = await typeService.DeleteCatalogTypeAsync(request.Id);
            var response = new GrpcTypeBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcCatalogType> GrpcGetCatalogTypeById(GrpcTypeIdRequest request, ServerCallContext context)
        {
            var type = await typeService.GetCatalogTypeByIdAsync(request.Id);
            var response = mapper.Map<GrpcCatalogType>(type);
            return response;
        }

        public override async Task<GrpcCatalogTypeList> GrpcGetCatalogTypes(GrpcTypeEmptyRequest request, ServerCallContext context)
        {
            var types = await typeService.GetCatalogTypesAsync();
            var brandList = mapper.Map<List<GrpcCatalogType>>(types);
            var response = new GrpcCatalogTypeList();
            brandList.ForEach(x => response.Types_.Add(x));
            return response;
        }
    }
}
