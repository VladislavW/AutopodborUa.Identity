using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutopodborUa.Identity.Areas.Account.ViewModels
{
    public class LogoutViewModel
    {
        public bool ShowLogoutPrompt { get; set; }
        public string LogoutId { get; set; }
    }
}
