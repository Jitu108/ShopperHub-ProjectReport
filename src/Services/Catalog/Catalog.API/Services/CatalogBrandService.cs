using AutoMapper;
using Catalog.API.Data.Interface;
using Catalog.API.Dtos;
using Catalog.API.Entities;

namespace Catalog.API.Services
{
    public class CatalogBrandService : ICatalogBrandService
    {
        private readonly ICatalogBrandRepo brandRepo;
        private readonly IMapper mapper;

        public CatalogBrandService(ICatalogBrandRepo brandRepo, IMapper mapper)
        {
            this.brandRepo = brandRepo;
            this.mapper = mapper;
        }

        public Task<bool> AddCatalogBrandAsync(CatalogBrandCreate brand)
        {
            var brandModel = mapper.Map<CatalogBrand>(brand);
            return brandRepo.AddCatalogBrandAsync(brandModel);
        }

        public Task<bool> UpdateCatalogBrandAsync(CatalogBrandUpdate brand)
        {
            var brandModel = mapper.Map<CatalogBrand>(brand);
            return brandRepo.UpdateCatalogBrandAsync(brandModel);
        }

        public Task<bool> DeleteCatalogBrandAsync(long brandId) =>
        brandRepo.DeleteCatalogBrandAsync(brandId);

        public async Task<CatalogBrandRead> GetCatalogBrandByIdAsync(long catalogBrandId)
        {
            var brand = await brandRepo.GetCatalogBrandByIdAsync(catalogBrandId);
            var brandDto = mapper.Map<CatalogBrandRead>(brand);
            return brandDto;
        }

        public async Task<IEnumerable<CatalogBrandRead>> GetCatalogBrandsAsync()
        {
            var brands = await brandRepo.GetCatalogBrandsAsync();
            var brandsDto = mapper.Map<IEnumerable<CatalogBrandRead>>(brands).ToList();
            return brandsDto;
        }
        
    }
}