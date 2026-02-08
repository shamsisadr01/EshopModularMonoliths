using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Service To Conatiner
            //services
            //    .AddApplicationServices()
            //    .AddInfrastructureServices(configuration)
            //    .AddApiServices(configuration)
            return services;
        }

        public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
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
