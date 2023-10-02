using AdminBff.Dtos;
using AdminBff.InterServiceCommunication.SyncDataClient;

namespace AdminBff.Services
{
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly ICatalogTypeClient typeClient;

        public CatalogTypeService(ICatalogTypeClient typeClient)
        {
            this.typeClient = typeClient;
        }
        public Task<bool> AddCatalogTypeAsync(CatalogTypeCreate catalogType)
        {
            return typeClient.AddCatalogTypeAsync(catalogType);
        }

        public Task<bool> DeleteCatalogTypeAsync(long catalogtypeId)
        {
            return typeClient.DeleteCatalogTypeAsync(catalogtypeId);
        }

        public Task<CatalogTypeRead> GetCatalogTypeByIdAsync(long catalogtypeId)
        {
            return typeClient.GetCatalogTypeByIdAsync(catalogtypeId);
        }

        public Task<IEnumerable<CatalogTypeRead>> GetCatalogTypesAsync()
        {
            return typeClient.GetCatalogTypesAsync();
        }

        public Task<bool> UpdateCatalogTypeAsync(CatalogTypeUpdate type)
        {
            return typeClient.UpdateCatalogTypeAsync(type);
        }
    }
}
