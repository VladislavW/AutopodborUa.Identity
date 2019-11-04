﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AutopodborUa.Identity.Infrastructure.Configuratios
{
    public static class IdentityServerConfig
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "Frank",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Underwood"),
                        new Claim("tenant", "whitehouse")
                    }
                },
                new TestUser
                {
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "Claire",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Claire"),
                        new Claim("family_name", "Underwood"),
                        new Claim("tenant", "whitehouse")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("samplesecondapi", "Sample Second API",
                    new string [] {"given_name", "family_name", "tenant"}),

                // api secret for reference token
                new ApiResource("sampleapi", "Sample API",
                    new string [] {"given_name", "family_name", "tenant"})
                {
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Sample Web Client",
                    ClientId = "samplewebclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AccessTokenType = AccessTokenType.Reference,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    FrontChannelLogoutUri = "https://localhost:44318/Home/IDPTriggeredLogout",

                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44318/signin-oidc",
                        "https://localhost:44319/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sampleapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    PostLogoutRedirectUris =
                    {
                        "https://localhost:44318/signout-callback-oidc",
                        "https://localhost:44319/signout-callback-oidc"
                    }
                },

                new Client
                {
                    ClientName = "Sample User Agent Client",
                    ClientId = "sampleuseragentclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris = new List<string>()
                    {
                        "https://localhost:4200/signin-oidc",
                        "https://localhost:4200/redirect-silentrenew"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sampleapi"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:4200/"
                    }
                },

                 new Client
                {
                    ClientName = "Sample Token Exchange Client",
                    ClientId = "sampletokenexchangeclient",
                    AllowedGrantTypes = new[] { "urn:ietf:params:oauth:grant-type:token-exchange" },
                    RequireConsent = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "samplesecondapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                 }
             };
        }
    }
}
