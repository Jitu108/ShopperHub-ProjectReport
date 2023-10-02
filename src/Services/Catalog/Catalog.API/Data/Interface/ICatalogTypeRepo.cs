using Catalog.API.Entities;

namespace Catalog.API.Data.Interface
{
    public interface ICatalogTypeRepo
    {
        Task<bool> AddCatalogTypeAsync(CatalogType catalogType);
        Task<bool> UpdateCatalogTypeAsync(CatalogType catalogType);
        Task<bool> DeleteCatalogTypeAsync(long typeId);
        Task<CatalogType> GetCatalogTypeByIdAsync(long catalogtypeId);
        Task<IEnumerable<CatalogType>> GetCatalogTypesAsync();
    }
}
