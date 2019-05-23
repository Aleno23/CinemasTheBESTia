﻿using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.IdentityServer.Configuration
{
    public static class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("CinemasTheBESTia.CinemaBooking.API", "Booking API"),
                new ApiResource("CinemasTheBESTia.Movies.API", "Movies API"),
                new ApiResource("CinemasTheBESTia.Web.MVC", "CinemasTheBESTia MVC")
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

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = configuration.GetValue<string>("ClientBooking:ClientID"),
                    ClientName= "Booking API",
                    AllowedGrantTypes = new List<string>(){GrantType.Hybrid, GrantType.ClientCredentials },
                    RequireConsent= false,

                    ClientSecrets =
                    {
                        new Secret(configuration.GetValue<string>("ClientBooking:SecretKey").Sha256())
                    },
                    RedirectUris = { $"{configuration.GetValue<string>("ClientBooking:Url")}signin-oidc"},
                    PostLogoutRedirectUris = {$"{configuration.GetValue<string>("ClientBooking:Url")}signout-callback-oidc"},

                    AllowedScopes = new List<string>                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CinemasTheBESTia.CinemaBooking.API",
                    },

                    AllowOfflineAccess= true,
                    AlwaysIncludeUserClaimsInIdToken= true
                },


                 new Client
                {
                    ClientId = configuration.GetValue<string>("ClientMovie:ClientID"),
                    ClientName= "Movies API",
                    AllowedGrantTypes = new List<string>(){GrantType.Hybrid, GrantType.ClientCredentials },
                    RequireConsent= false,

                    ClientSecrets =
                    {
                        new Secret(configuration.GetValue<string>("ClientMovie:SecretKey").Sha256())
                    },
                    RedirectUris = { $"{configuration.GetValue<string>("ClientMovie:Url")}signin-oidc"},
                    PostLogoutRedirectUris = {$"{configuration.GetValue<string>("ClientMovie:Url")}signout-callback-oidc"},

                    AllowedScopes = new List<string>                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CinemasTheBESTia.Movies.API",
                    },

                    AllowOfflineAccess= true,
                    AlwaysIncludeUserClaimsInIdToken= true
                },



                  new Client
                {
                    ClientId = configuration.GetValue<string>("ClientMVC:ClientID"),
                    ClientName= "CinemasTheBESTia MVC",
                    AllowedGrantTypes = new List<string>(){GrantType.Hybrid, GrantType.ClientCredentials },
                    RequireConsent= false,

                    ClientSecrets =
                    {
                        new Secret(configuration.GetValue<string>("ClientMVC:SecretKey").Sha256())
                    },
                    RedirectUris = { $"{configuration.GetValue<string>("ClientMVC:Url")}signin-oidc"},
                    PostLogoutRedirectUris = {$"{configuration.GetValue<string>("ClientMVC:Url")}signout-callback-oidc"},

                    AllowedScopes = new List<string>                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CinemasTheBESTia.Web.MVC",
                    },

                    AllowOfflineAccess= true,
                    AlwaysIncludeUserClaimsInIdToken= true
                }
            };
        }
    }
}
