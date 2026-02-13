
namespace Catalog.Data.Seed
{
    public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
    {
        private readonly CatalogDbContext _dbContext = dbContext;

        public async Task SeedAllAsync()
        {
            if(!await _dbContext.Products.AnyAsync())
            {
                await _dbContext.Products.AddRangeAsync(InitialData.Products);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
