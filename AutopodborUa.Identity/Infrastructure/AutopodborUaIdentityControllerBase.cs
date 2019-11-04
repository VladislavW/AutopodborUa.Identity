using AutopodborUa.Identity.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Infrastructure
{
    [SecurityHeaders]
    public class AutopodborUaIdentityControllerBase : Controller
    {
    }
}
