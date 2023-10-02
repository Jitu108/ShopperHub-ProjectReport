using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.SqlDataStore
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<CatalogBrand>().ToTable("CatalogBrand");
            modelBuilder.Entity<CatalogType>().ToTable("CatalogType");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Image>().ToTable("Image");

            modelBuilder.Entity<Image>()
            .HasOne(x => x.Product)
            .WithOne()
            .HasForeignKey<Image>(x => x.ProductId);

            modelBuilder.Entity<Product>()
            .HasOne(x => x.CatalogBrand)
            .WithMany()
            .HasForeignKey(x => x.CatalogBrandId);

            modelBuilder.Entity<Product>()
            .HasOne(x => x.CatalogType)
            .WithMany()
            .HasForeignKey(x => x.CatalogTypeId);

            modelBuilder.Entity<Product>()
            .HasOne(x => x.Image)
            .WithOne(x => x.Product);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
