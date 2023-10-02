using Catalog.API.Entities;

namespace Catalog.API.Data.Interface
{
    public interface ICatalogBrandRepo
    {
        Task<bool> AddCatalogBrandAsync(CatalogBrand brand);
        Task<bool> UpdateCatalogBrandAsync(CatalogBrand brand);
        Task<bool> DeleteCatalogBrandAsync(long brandId);
        Task<CatalogBrand> GetCatalogBrandByIdAsync(long catalogBrandId);
        Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync();
    }
}
