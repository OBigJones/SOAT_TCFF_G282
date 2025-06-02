using Application.Repository;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infra.Pag.Extensions;

public static class MercadoPagoInjection
{
    public static IServiceCollection AddMercadoPagoConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MecadoPago").GetSection("Authorization").Value;

        // services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<MercadoPagoClient>(provider => new MercadoPagoClient(configuration));

        return services;
    }
}