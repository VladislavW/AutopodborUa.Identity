using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutopodborUa.Identity.Infrastructure.Validators;
using AutopodborUa.Identity.Infrastructure.Configuratios;

namespace AutopodborUa.Identity.Infrastructure.ServiceCollectionExtensions
{
    public static class IdentityServerExtension
    {
        public static IServiceCollection AddIdentityServerCustom(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var identityServerDBConnectionString = configuration
                .GetSection("ConnectionStrings:IdentityServerDBConnectionString")
                .Get<string>();

            services.AddIdentityServer()
              .AddDeveloperSigningCredential()
              .AddTestUsers(IdentityServerConfig.GetUsers())
              .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
              .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
              .AddInMemoryClients(IdentityServerConfig.GetClients())
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder =>
                      builder.UseSqlServer(identityServerDBConnectionString,
                          sql => sql
                           .EnableRetryOnFailure()
                           .MigrationsAssembly("AutopodborUa.Identity.Storage"));
                  options.EnableTokenCleanup = true;
              })
              .AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>();

            return services;
        }
    }
}
