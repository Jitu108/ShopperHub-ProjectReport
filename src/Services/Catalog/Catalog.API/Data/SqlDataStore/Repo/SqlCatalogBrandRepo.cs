using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.SqlDataStore.Repo
{
    public class SqlCatalogBrandRepo : ICatalogBrandRepo
    {
        private readonly CatalogDbContext context;

        public SqlCatalogBrandRepo(CatalogDbContext context)
        {
            this.context = context;
        }


        public async Task<bool> AddCatalogBrandAsync(CatalogBrand brand)
        {
            await context.CatalogBrands.AddAsync(brand);
            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> UpdateCatalogBrandAsync(CatalogBrand brand)
        {
            context.CatalogBrands.Attach(brand);
            context.Entry(brand).State = EntityState.Modified;
            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> DeleteCatalogBrandAsync(long brandId)
        {
            var brandInDb = await context.CatalogBrands.Where(x => x.Id == brandId).FirstOrDefaultAsync();
            if (brandInDb != null)
            {
                context.CatalogBrands.Remove(brandInDb);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CatalogBrand> GetCatalogBrandByIdAsync(long catalogBrandId)
        {
            return await context.CatalogBrands.Where(x => x.Id == catalogBrandId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync()
        {
            return await context.CatalogBrands.ToListAsync();
        }

    }
}
