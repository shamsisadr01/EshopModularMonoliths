namespace Basket.Data
{
    // Add-Migration InitialCreate -OutputDir Data/Migrations -Project Basket -StartupProject Api -Context BasketDbContext
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options) 
        {
            
        }

        public DbSet<ShoppingCart> ShoppingCarts=> Set<ShoppingCart>();

        public DbSet<ShoppingCartItem> ShoppingCartItems=> Set<ShoppingCartItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("basket");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
