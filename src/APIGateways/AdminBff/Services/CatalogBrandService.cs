using AdminBff.Dtos;
using AdminBff.InterServiceCommunication.SyncDataClient;

namespace AdminBff.Services
{
    public class CatalogBrandService : ICatalogBrandService
    {
        private readonly ICatalogBrandClient brandClient;

        public CatalogBrandService(ICatalogBrandClient brandClient)
        {
            this.brandClient = brandClient;
        }

        public Task<bool> AddCatalogBrandAsync(CatalogBrandCreate brand)
        {
            return brandClient.AddCatalogBrandAsync(brand);
        }

        public Task<bool> DeleteCatalogBrandAsync(long brandId)
        {
            return brandClient.DeleteCatalogBrandAsync(brandId);
        }

        public Task<CatalogBrandRead> GetCatalogBrandByIdAsync(long catalogBrandId)
        {
            return brandClient.GetCatalogBrandByIdAsync(catalogBrandId);
        }

        public Task<IEnumerable<CatalogBrandRead>> GetCatalogBrandsAsync()
        {
            return brandClient.GetCatalogBrandsAsync();
        }

        public Task<bool> UpdateCatalogBrandAsync(CatalogBrandUpdate brand)
        {
            return brandClient.UpdateCatalogBrandAsync(brand);
        }
    }
}
