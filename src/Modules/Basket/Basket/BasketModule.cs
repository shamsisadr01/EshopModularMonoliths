using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Service To Conatiner
            //services
            //    .AddApplicationServices()
            //    .AddInfrastructureServices(configuration)
            //    .AddApiServices(configuration)
            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // Add Service To Conatiner
            //services
            //    .UseApplicationServices()
            //    .UseInfrastructureServices(configuration)
            //    .UseApiServices(configuration)
            return app;
        }
    }
}
