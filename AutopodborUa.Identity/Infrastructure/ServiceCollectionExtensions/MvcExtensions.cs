using AutopodborUa.Identity.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Infrastructure.ServiceCollectionExtensions
{
    internal static class MvcExtensions
    {
        public static void AddMvcCustom(this IServiceCollection services)
        {

            services.AddMvcCore();
            services.AddMvc(options =>
            {
               // options.Filters.Add<ValidateModelStateAttribute>();
              //  options.Filters.Add<ExceptionHandlerAttribute>();
                options.Filters.Add<InternalServerErrorFilterAttribute>(-1);
            });
        }
    }
}
