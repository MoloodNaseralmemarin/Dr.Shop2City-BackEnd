using DrShop2City.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrShop2City.Infrastructure.Utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddDbContext<DrShop2CityDBContext>(options =>
            {
                var connectionString = "ConnectionStrings:DrShop2CityConnection:Development";
                options.UseSqlServer(configuration[connectionString]);
            });

            return service;
        }
    }
}
