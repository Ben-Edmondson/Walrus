using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

namespace WalrusClassLibrary.Auth
{
    public class AuthenticationService(B2CConnectionDetails config) : IAuthService
    {
        private readonly IPublicClientApplication _pca = PublicClientApplicationBuilder.Create(config.ClientId)
            .WithRedirectUri("msal65cd967a-5267-44ed-9ccd-657f36110b59://auth")
            .Build();

        private readonly string[] _scopes = ["openid", "offline_access"];

        public async Task<AuthenticationResult> SignInAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pca
                    .AcquireTokenInteractive(_scopes)
                    .WithPrompt(Prompt.ForceLogin) 
                    .ExecuteAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SignOutAsync()
        {
            var accounts = await _pca.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await _pca.RemoveAsync(account);
            }
        }
    }
}
