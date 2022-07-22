using IdentityManagment.Constants;
using IdentityManagment.Definitions;
using IdentityManagment.Extensions;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class ClientDataSeedService : ITransientDependency
    {
        private readonly IClientRepository _clientRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        private const string AUTH_URL = "https://localhost:42000";

        private string[] AUTH_URLS = { AUTH_URL };

        public ClientDataSeedService(IClientRepository clientRepository,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder)
        {
            _clientRepository = clientRepository;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
        }

        public virtual async Task SeedAsync()
        {
            const string commonSecret = "1q2w3e*";

            var commonScopes = new[] { "email", "openid", "profile", "role", "phone", "address" };

            // services - gateways
            await Add_Identity_Service_Swagger(commonSecret, commonScopes);
            await Add_User_Service_Swagger(commonSecret, commonScopes);
            await Add_Identity_Management_Swagger(commonSecret, commonScopes);

            // gateways
            await Add_Public_Gateway_Swagger(commonSecret, commonScopes);

            // client apps
            await Add_Client_React_App(commonScopes);
        }

        private async Task Add_Identity_Service_Swagger(string commonSecret, string[] commonScopes)
        {
            await CreateClientAsync(clientId: MicroserviceClientId_Swagger.ApiService.Identity,
                scopes: commonScopes.Union(new[]
                {
                    MicroserviceApiScopes.Identity,
                    MicroserviceApiScopes.Gateway.Internal
                }),
                grantTypes: ClientGrantTypeConstants.Code,
                secret: commonSecret,
                permissions: null, //  VendorServicePermission.GetAll(),
                redirectUri: "http://localhost:44550//swagger/oauth2-redirect.html",
                postLogoutRedirectUri: "http://localhost:44550//signout-callback-oidc",
                requiteClientSecret: true,
                requirePkce: false,
                allowedCorsOrigin: AUTH_URLS);
        }

        private async Task Add_User_Service_Swagger(string commonSecret, string[] commonScopes)
        {
            await CreateClientAsync(clientId: MicroserviceClientId_Swagger.ApiService.User,
                scopes: commonScopes.Union(new[]
                {
                    MicroserviceApiScopes.User,
                    MicroserviceApiScopes.Gateway.Internal
                }),
                grantTypes: ClientGrantTypeConstants.Code,
                secret: commonSecret,
                permissions: null, //  VendorServicePermission.GetAll(),
                redirectUri: "http://localhost:44356/swagger/oauth2-redirect.html",
                postLogoutRedirectUri: "http://localhost:44356/signout-callback-oidc",
                requiteClientSecret: true,
                requirePkce: false,
                allowedCorsOrigin: AUTH_URLS);
        }

        private async Task Add_Identity_Management_Swagger(string commonSecret, string[] commonScopes)
        {
            await CreateClientAsync(clientId: MicroserviceClientId_Swagger.ApiService.IdentityManagement,
                scopes: commonScopes.Union(new[]
                {
                    MicroserviceApiScopes.IdentityManagement,
                    MicroserviceApiScopes.Gateway.Internal
                }),
                grantTypes: ClientGrantTypeConstants.Code,
                secret: commonSecret,
                permissions: null, //  VendorServicePermission.GetAll(),
                redirectUri: "http://localhost:44550/swagger/oauth2-redirect.html",
                postLogoutRedirectUri: "http://localhost:44550/signout-callback-oidc",
                requiteClientSecret: true,
                requirePkce: false,
                allowedCorsOrigin: AUTH_URLS);
        }

        private async Task Add_Public_Gateway_Swagger(string commonSecret, string[] commonScopes)
        {
            await CreateClientAsync(clientId: MicroserviceClientId_Swagger.Gateway.Public,
                scopes: commonScopes.Union(new[]
                {
                    MicroserviceApiScopes.Identity,
                    MicroserviceApiScopes.IdentityManagement,
                    MicroserviceApiScopes.User,
                    MicroserviceApiScopes.Gateway.Internal,
                }),
                grantTypes: ClientGrantTypeConstants.Code,
                secret: commonSecret,
                permissions: IdentityPermissions.GetAll(),
                redirectUri: "https://192.168.40.245:8301/swagger/oauth2-redirect.html",
                postLogoutRedirectUri: "https://192.168.40.245:8301/signout-callback-oidc",
                requiteClientSecret: true,
                requirePkce: false,
                allowedCorsOrigin: AUTH_URLS);
        }

        private async Task Add_Client_React_App(string[] commonScopes)
        {
            var internalServiceGatewayApiScope = new[]
            {
                MicroserviceApiScopes.Gateway.Public,
                MicroserviceApiScopes.Identity,
                MicroserviceApiScopes.IdentityManagement,
                MicroserviceApiScopes.User,
                ClaimTypeConstants.permission
            };

            await CreateClientAsync(clientId: MicroserviceClientId_ClientApp.Client,
                scopes: commonScopes.Union(internalServiceGatewayApiScope),
                grantTypes: ClientGrantTypeConstants.Code,
                secret: "",
                permissions: null,
                redirectUri: "https://192.168.13.220:8404/api/auth/callback/identity-server",
                postLogoutRedirectUri: "https://192.168.13.220:8402",
                requiteClientSecret: false,
                requirePkce: true,
                allowedCorsOrigin: AUTH_URLS);
        }

        /// <summary>
        /// create clients
        /// </summary>
        /// <param name="absoluteRefreshTokenLifetime"> </param>
        private async Task<Client> CreateClientAsync(string clientId, IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            IEnumerable<string> permissions = null,
            bool requiteClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> allowedCorsOrigin = null,
            int identityTokenLifetime = 300, // 5 minutes
            int accessTokenLifetime = 31536000, //365 days
            int authorizationCodeLifetime = 300, // 5 minutes
            int absoluteRefreshTokenLifetime = 31536000, //365 days,
            int slidingRefreshTokenLifetime = 1296000)
        {
            var client = await _clientRepository.FindByClientIdAsync(clientId);

            if (client == null)
            {
                client = new Client(_guidGenerator.Create(), clientId)
                {
                    ClientName = clientId,
                    ProtocolType = "oidc",
                    Description = clientId,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true, // this input enable allowOfflineAccess
                    IdentityTokenLifetime = identityTokenLifetime, // in seconds
                    AccessTokenLifetime = accessTokenLifetime, // in seconds
                    AuthorizationCodeLifetime = authorizationCodeLifetime,
                    AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime,
                    SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime,
                    RequireConsent = false,
                    RequireClientSecret = requiteClientSecret,
                    RequirePkce = requirePkce
                };

                client = await _clientRepository.InsertAsync(client, autoSave: true);
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (client.FindSecret(secret.EncodeWithSha256()) == null)
            {
                client.AddSecret(secret.EncodeWithSha256());
            }

            if (redirectUri is not null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri is not null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            foreach (var corsOrigin in allowedCorsOrigin)
            {
                if (allowedCorsOrigin is not null)
                {
                    if (client.FindCorsOrigin(corsOrigin) == null)
                    {
                        client.AddCorsOrigin(corsOrigin);
                    }
                }
            }

            if (permissions is not null)
            {
                await _permissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName, clientId,
                    permissions);
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }
}