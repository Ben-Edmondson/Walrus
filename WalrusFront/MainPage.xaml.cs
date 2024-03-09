using System.Text;
using System.Threading.Tasks;
using WalrusClassLibrary.Auth;
using System.IdentityModel.Tokens.Jwt;


namespace WalrusFront
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthenticationService _authService;
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
                var token = result?.IdToken;
                if (token == null) return;
                var handler = new JwtSecurityTokenHandler();
                var data = handler.ReadJwtToken(token);
                await Navigation.PushAsync(new LandingPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Error", "Authentication failed: " + ex.Message, "OK");
            }
        }
    }

}
