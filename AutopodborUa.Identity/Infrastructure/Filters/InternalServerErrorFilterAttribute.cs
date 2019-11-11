using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Infrastructure.Filters
{
    internal sealed class InternalServerErrorFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public InternalServerErrorFilterAttribute(
            IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            if (_hostingEnvironment.IsDevelopment())
            {
                context.Result = new JsonResult(new
                {
                    message = context.Exception.Message,
                    stacktrace = context.Exception.StackTrace,
                    exception = context.Exception
                });
            }

            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
        }
    }
}
