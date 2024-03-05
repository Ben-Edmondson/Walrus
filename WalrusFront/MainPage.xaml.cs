using System.Text;
using System.Threading.Tasks;
using WalrusClassLibrary.Auth;
using System.IdentityModel.Tokens.Jwt;


namespace WalrusFront
{
    public partial class MainPage : ContentPage
    {
        private AuthenticationService _authService;
        public MainPage(AuthenticationService authenticationService)
        {
            InitializeComponent();
            _authService = authenticationService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await _authService.SignInAsync(CancellationToken.None);
                var token = result?.IdToken; // AccessToken also can be used
                if (token != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var data = handler.ReadJwtToken(token); }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                await DisplayAlert("Login Error", "Authentication failed: " + ex.Message, "OK");
            }
        }
    }

}
