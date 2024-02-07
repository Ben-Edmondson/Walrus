using Newtonsoft.Json;
using System.Text;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin.Auth;
using Walrus_Class_Library.Models;
using Walrus_Class_Library.Repository;

namespace Walrus_Class_Library.Services
{
    public class Authentication
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _apiKey;
        private readonly FirebaseIntegration _firebaseIntegration;

        public Authentication(IConfiguration configuration, FirebaseClient firebaseClient, FirebaseIntegration firebaseIntegration)
        {
            _apiKey = configuration["Firebase:ApiKey"];
            _firebaseClient = firebaseClient;
            _firebaseIntegration = firebaseIntegration;
        }

        public async Task<HttpResponseMessage> RegisterUser(string email, string password)
        {
            using var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                email,
                password,
                returnSecureToken = true
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_apiKey}", content);
            return response; 
        }

        public async Task<AuthenticateUserViewModel> LoginUser(string email, string password)
        {
            using var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                email,
                password,
                returnSecureToken = true
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}", content);

            var model = new AuthenticateUserViewModel
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<FetchUserDataOnLoginViewModel>(responseContent); 
                model.User = new UserPresenceViewModel
                {
                    UID = userInfo.LocalId,
                    ConnectionStatus = true,
                    LastOnline = DateTime.UtcNow 
                };
                _firebaseIntegration.WriteData(_firebaseClient, model.User);
            }

            return model;
        }
    }
}
