using AdminBff.Dtos;

namespace AdminBff.InterServiceCommunication.SyncDataClient
{
    public interface ICatalogTypeClient
    {
        Task<bool> AddCatalogTypeAsync(CatalogTypeCreate catalogType);
        Task<bool> UpdateCatalogTypeAsync(CatalogTypeUpdate type);
        Task<bool> DeleteCatalogTypeAsync(long catalogtypeId);
        Task<CatalogTypeRead> GetCatalogTypeByIdAsync(long catalogtypeId);
        Task<IEnumerable<CatalogTypeRead>> GetCatalogTypesAsync();
    }
}
