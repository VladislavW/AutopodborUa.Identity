using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Infrastructure.ServiceCollectionExtensions
{
    public static class OpenIdExternalExtension
    {
        public static IServiceCollection AddGoogleOpenIdConnectCustom(
          this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddAuthentication()
           .AddOpenIdConnect("Google", "Google",
               o =>
               {
                   IConfigurationSection googleAuthNSection =
                       configuration.GetSection("Authentication:Google");
                   o.ClientId = googleAuthNSection["ClientId"];
                   o.ClientSecret = googleAuthNSection["ClientSecret"];
                   o.Authority = "https://accounts.google.com";
                   o.ResponseType = OpenIdConnectResponseType.Code;
                   o.CallbackPath = "/signin-google";
               })
           .AddIdentityServerJwt();

            return services;
        }
    }
}
