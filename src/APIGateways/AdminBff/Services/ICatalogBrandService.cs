using AdminBff.Dtos;

namespace AdminBff.Services
{
    public interface ICatalogBrandService
    {
        Task<bool> AddCatalogBrandAsync(CatalogBrandCreate brand);
        Task<bool> UpdateCatalogBrandAsync(CatalogBrandUpdate brand);
        Task<bool> DeleteCatalogBrandAsync(long brandId);
        Task<CatalogBrandRead> GetCatalogBrandByIdAsync(long catalogBrandId);
        Task<IEnumerable<CatalogBrandRead>> GetCatalogBrandsAsync();
    }
}
