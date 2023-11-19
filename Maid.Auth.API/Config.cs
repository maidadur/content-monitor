using IdentityServer4.Models;
using System.Collections.Generic;

namespace Maid.Auth.API
{
	public class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources() {
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiResource> GetApiResources() {
			return new List<ApiResource>
			{
				new ApiResource("client", "Resource API")
				{
					Scopes = new [] { "api" }
				}
			};
		}

		public static IEnumerable<Client> GetClients(string uiUrl) {
			return new[]
			{
				new Client
				{
					ClientId = "client",
					ClientName = "Api client",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					//ClientSecrets =
					//{
					//	new Secret("secret".Sha256())
					//},
					AllowedScopes = { "api" }
				},
				new Client {
					RequireConsent = false,
					ClientId = "angular_spa",
					ClientName = "Angular SPA",
					AllowedGrantTypes = GrantTypes.Code,
					RequireClientSecret = false,
					AllowedScopes = { "openid", "profile", "email", "api" },
					RedirectUris = { $"{uiUrl}/auth-callback", $"{uiUrl}/auth/silent-refresh"},
					PostLogoutRedirectUris = {uiUrl},
					AllowedCorsOrigins = {uiUrl},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				}
			};
		}
	}
}