using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastruture(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MysqlConnection");
            services.AddDbContext<AddDbContext>(options => options.UseMysql(connectionString, ServerVersion.AutoDetect(connectionString)));

            return services;
        }

    }
}