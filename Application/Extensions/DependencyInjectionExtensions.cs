using Application.Repository;
using Application.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            // services.AddSingleton<IUserRepository>();
            return services;
        }
    }
}
