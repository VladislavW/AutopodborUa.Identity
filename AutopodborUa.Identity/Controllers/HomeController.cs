using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutopodborUa.Identity.Models;
using AutopodborUa.Identity.Infrastructure;
using IdentityServer4.Services;

namespace AutopodborUa.Identity.Controllers
{
    public class HomeController : AutopodborUaIdentityControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(ILogger<HomeController> logger, IIdentityServerInteractionService interaction)
        {
            _logger = logger;
            _interaction = interaction;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}
