using Firebase.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Walrus_Class_Library.Services
{
    public class InitialiseFirebase
    {
        private FirebaseClient firebaseClient;
        private IConfiguration config;

        public InitialiseFirebase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("keys.json", optional: false, reloadOnChange: true);
            config = builder.Build();
        }

        public FirebaseClient Initialise()
        {
            var firebaseUrl = config["Firebase:Url"];
            var authToken = config["Firebase:AuthToken"];

            firebaseClient = new FirebaseClient(
                firebaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authToken)
                });
            return firebaseClient;
        }
    }
}