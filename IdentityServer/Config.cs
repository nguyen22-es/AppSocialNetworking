using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkingApi.IdentityServer
{
    public class Config
    {
        // định nghĩa các Resource gì  // cho user quản lý   // chả về 1 danh sách các identityresource
        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              // user quản lý nhứng gì này // chuẩn của identity ít nhất phải có 2 thằng này
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
              
              //vd:
              // new IdentityResources.Email(),
              // new IdentityResources.Phone()
          };

        // danh sách các Api ở đây ta chỉ có mỗi thằng knowledgespace
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {

                 new ApiResource("CoffeeAPI.read","CoffeeAPI.write")
            };



        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
                new ApiScope("CoffeeAPI.read","CoffeeAPI.write"),
               
        };



        /*  định nghĩa ra các Client chín là các ứng dụng ta định làm , chính là webportal , server (chính là swagger) và .. */

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
             new Client
        {
            ClientId = "cwm.client",
            ClientName = "Client Credentials Client",
              AllowedCorsOrigins =     { "https://localhost:5444" },
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "CoffeeAPI.read" }
        },
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                       AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                       AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CoffeeAPI.read"
                    }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                   AllowAccessTokensViaBrowser = false,
                       AllowedCorsOrigins =     { "https://localhost:5444" },
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                          AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CoffeeAPI.read"
                    },
                    RequirePkce = true,
                    RequireConsent = false,
                    AllowPlainTextPkce = false
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                      ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    RedirectUris =           { "https://localhost:5000/swagger/oauth2-redirect.html" }, // chuyển hướng
                    PostLogoutRedirectUris = { "https://localhost:5000/swagger/oauth2-redirect.html" },// chuyển hướng đăng xuất
                    AllowedCorsOrigins =     { "https://localhost:5000" }, // cho phép nguồn gốc cores

                  
                    
                       
                         AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CoffeeAPI.read"
                    }

                },
               
            };
    }
}
