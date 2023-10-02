using AutoMapper;
using Catalog.API.Data.Interface;
using Catalog.API.Dtos;
using Catalog.API.Entities;

namespace Catalog.API.Services
{
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly ICatalogTypeRepo typeRepo;
        private readonly IMapper mapper;

        public CatalogTypeService(ICatalogTypeRepo typeRepo, IMapper mapper)
        {
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        public Task<bool> AddCatalogTypeAsync(CatalogTypeCreate catalogType)
        {
            var typeModel = mapper.Map<CatalogType>(catalogType);
            return typeRepo.AddCatalogTypeAsync(typeModel);
        }

        public Task<bool> UpdateCatalogTypeAsync(CatalogTypeUpdate type)
        {
            var typeModel = mapper.Map<CatalogType>(type);
            return typeRepo.UpdateCatalogTypeAsync(typeModel);
        }
        

        public Task<bool> DeleteCatalogTypeAsync(long catalogtypeId)
        {
            return typeRepo.DeleteCatalogTypeAsync(catalogtypeId);
        }

        public async Task<CatalogTypeRead> GetCatalogTypeByIdAsync(long catalogtypeId)
        {
            var type = await typeRepo.GetCatalogTypeByIdAsync(catalogtypeId);
            var typesDto = mapper.Map<CatalogTypeRead>(type);
            return typesDto;
        }
        
        public async Task<IEnumerable<CatalogTypeRead>> GetCatalogTypesAsync()
        {
            var types = await typeRepo.GetCatalogTypesAsync();
            var typesDto = mapper.Map<IEnumerable<CatalogTypeRead>>(types);
            return typesDto;
        }
    }
}