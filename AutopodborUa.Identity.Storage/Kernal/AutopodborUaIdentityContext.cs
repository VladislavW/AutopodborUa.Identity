using AutopodborUa.Identity.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AutopodborUa.Identity.Storage.Kernal
{
    public class AutopodborUaIdentityContext : ApiAuthorizationDbContext<User>
    {
        public AutopodborUaIdentityContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
    }
}
