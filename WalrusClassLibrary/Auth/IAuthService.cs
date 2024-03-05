
using Microsoft.Identity.Client;

namespace WalrusClassLibrary.Auth
{
    public interface IAuthService
    {
        public Task<AuthenticationResult> SignInAsync(CancellationToken cancellationToken);
        public Task SignOutAsync();
    }
}
