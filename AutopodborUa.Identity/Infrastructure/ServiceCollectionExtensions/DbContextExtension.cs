using AutopodborUa.Identity.Storage.Kernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Infrastructure.ServiceCollectionExtensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddAutopodborUaIdentityContext(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var identityServerDBConnectionString = configuration
                .GetSection("ConnectionStrings:IdentityServerDBConnectionString")
                .Get<string>();

            services.AddDbContext<AutopodborUaIdentityContext>(options => options
                        .UseSqlServer(identityServerDBConnectionString,
                          sql => sql
                           .EnableRetryOnFailure()
                           .MigrationsAssembly("AutopodborUa.Identity.Storage")));

            return services;
        }
    }
}
