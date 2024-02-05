using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Walrus_Class_Library.Services
{
    public class Authentication
    {
        private readonly string apiKey;

        public Authentication(IConfiguration configuration)
        {
            this.apiKey = configuration["Firebase:ApiKey"];
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
                $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}", content);
            return response; 
        }

        public async Task<HttpResponseMessage> LoginUser(string email, string password)
        {
            using var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                email,
                password,
                returnSecureToken = true
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", content);
            return response; // This result will contain the ID token and refresh token.
        }
    }
}
