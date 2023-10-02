using UserBff.Dtos;

namespace UserBff.Services
{
    public interface ICatalogTypeService
    {
        Task<bool> AddCatalogTypeAsync(CatalogTypeCreate catalogType);
        Task<bool> UpdateCatalogTypeAsync(CatalogTypeUpdate type);
        Task<bool> DeleteCatalogTypeAsync(long catalogtypeId);
        Task<CatalogTypeRead> GetCatalogTypeByIdAsync(long catalogtypeId);
        Task<IEnumerable<CatalogTypeRead>> GetCatalogTypesAsync();
    }
}
