using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutopodborUa.Identity.Infrastructure.Validators;
using AutopodborUa.Identity.Storage.Kernal;
using AutopodborUa.Identity.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.Models;
using System.Linq;
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

            services.AddDefaultIdentity<User>()
               .AddEntityFrameworkStores<AutopodborUaIdentityContext>();

            services.AddIdentityServer()
                .AddApiAuthorizationCustom()
                .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder =>
                      builder.UseSqlServer(identityServerDBConnectionString,
                          sql => sql
                           .EnableRetryOnFailure()
                           .MigrationsAssembly("AutopodborUa.Identity.Storage"));
                  options.EnableTokenCleanup = true;
                  //options.TokenCleanupInterval = 30; //interval in seconds
              })
               .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
               .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
               .AddInMemoryClients(IdentityServerConfig.GetClients())
               .AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>();


            //.AddDeveloperSigningCredential()
            //.AddTestUsers(IdentityServerConfig.GetUsers())
            //.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            //.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
            //.AddInMemoryClients(IdentityServerConfig.GetClients())


            //  .AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseSqlServer(identityServerDBConnectionString,
            //            sql => sql
            //             .EnableRetryOnFailure()
            //             .MigrationsAssembly("AutopodborUa.Identity.Storage"));
            //    options.EnableTokenCleanup = true;
            //    //options.TokenCleanupInterval = 30; //interval in seconds
            //})
            //.AddConfigurationStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseSqlServer(identityServerDBConnectionString,
            //            sql => sql
            //            .EnableRetryOnFailure()
            //           .MigrationsAssembly("AutopodborUa.Identity.Storage"));
            //})


            return services;
        }

        private static IIdentityServerBuilder AddApiAuthorizationCustom(this IIdentityServerBuilder builder)
        {
            builder
                .AddApiAuthorization<User, AutopodborUaIdentityContext>(options =>
            {
                var apiResource = options.ApiResources.First();
                apiResource.UserClaims = new[] { "hasUsersGroup" };

                var identityResource = new IdentityResource
                {
                    Name = "customprofile",
                    DisplayName = "Custom profile",
                    UserClaims = new[] { "hasUsersGroup" },
                };
                identityResource.Properties.Add(ApplicationProfilesPropertyNames.Clients, "*");
                options.IdentityResources.Add(identityResource);
            });

            return builder;
        }
    }
}
