
using Basket.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptor;
using Shared.Data.Seed;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Service To Conatiner

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CachedBasketRepository>();
            //services.AddScoped<IBasketRepository>(sp =>
            //{
            //    var basketRepository = sp.GetRequiredService<IBasketRepository>();
            //    return new CachedBasketRepository(sp.GetRequiredService<IDistributedCache>(), basketRepository);
            //});

            // Data - Infrastructure Services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

          //  services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // Configure the request pipline

            // 1. Use Api Endpoint Services

            // 2. Use Application Use Case Services

            // 3. UseData - Infrastructure Services
            app.UseMigration<BasketDbContext>();
            return app;
        }
    }
}
