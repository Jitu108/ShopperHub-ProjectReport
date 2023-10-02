using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.SqlDataStore.Repo
{
    public class SqlCatalogTypeRepo : ICatalogTypeRepo
    {
        private readonly CatalogDbContext context;

        public SqlCatalogTypeRepo(CatalogDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddCatalogTypeAsync(CatalogType catalogType)
        {
            await context.CatalogTypes.AddAsync(catalogType);
            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> UpdateCatalogTypeAsync(CatalogType type)
        {
            context.CatalogTypes.Attach(type);
            context.Entry<CatalogType>(type).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCatalogTypeAsync(long typeId)
        {
            var typeInDb = await context.CatalogTypes.Where(x => x.Id == typeId).FirstOrDefaultAsync();
            if (typeInDb != null)
            {
                context.CatalogTypes.Remove(typeInDb);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CatalogType> GetCatalogTypeByIdAsync(long catalogtypeId)
        {
            return await context.CatalogTypes.Where(x => x.Id == catalogtypeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CatalogType>> GetCatalogTypesAsync()
        {
            return await context.CatalogTypes.ToListAsync();
        }


    }
}
