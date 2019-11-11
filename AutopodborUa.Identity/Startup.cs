using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutopodborUa.Identity.Infrastructure.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AutopodborUa.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvcCustom();
            services.AddAutopodborUaIdentityContext(Configuration);
            services.AddIdentityServerCustom(Configuration);
            services.AddGoogleOpenIdConnectCustom(Configuration);
           // services.AddControllersWithViews(); 

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ShouldHasUsersGroup", policy => { policy.RequireClaim("hasUsersGroup"); });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseHsts();
            }

            
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{returnUrl?}");
            });
        }
    }
}
