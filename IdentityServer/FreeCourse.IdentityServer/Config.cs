// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResource => new ApiResource[]
        {
          new ApiResource("resource_catalog"){Scopes={"catqalog_fulpermission"}},
          new ApiResource("photo_stock_catalog"){Scopes={"photo_stock_fulpermission"}},
          new ApiResource("resource_basket"){Scopes={"basket_fulpermission"}},
          new ApiResource("resource_discount"){Scopes={"discount_fulpermission"}},
          new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };




        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.Email(),
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Roles", UserClaims= new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catqalog_fulpermission","Catalog API full access"),
                new ApiScope("photo_stock_fulpermission","Photo Stock API full access"),
                new ApiScope("basket_fulpermission","Basket API full access"),
                new ApiScope("discount_fulpermission","Discount API full access"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client 
               {
                 ClientName ="Asp.Net Core MVC",
                 ClientId = "WebMVCClient",
                 //AllowOfflineAccess = true,
                 ClientSecrets={new Secret("secret".Sha256())},
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 AllowedScopes = { "catqalog_fulpermission", "photo_stock_fulpermission",IdentityServerConstants.LocalApi.ScopeName }
               },
                new Client
               {
                 ClientName ="Asp.Net Core MVC",
                 ClientId = "WebMvcClientForUser",
                 AllowOfflineAccess = true,
                 ClientSecrets={new Secret("secret".Sha256())},
                 AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                 AllowedScopes = { "basket_fulpermission", "discount_fulpermission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "roles"},
                 AccessTokenLifetime = 1*60*60,
                 RefreshTokenExpiration=TokenExpiration.Absolute,
                 AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                 RefreshTokenUsage=TokenUsage.ReUse
               }
            };
    }
}