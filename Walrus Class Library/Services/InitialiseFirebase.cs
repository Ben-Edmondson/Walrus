
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Walrus_Class_Library.Services
{
    public class InitialiseFirebase
    {
        string pathToKey = "C:/Users/bened/Desktop/Uni/Dissertation/Walrus/Walrus Class Library/Services/walrus-SDK-Account-Key.json";
        public void Initialise()
        {
            var credentials = GoogleCredential.FromFile(pathToKey);
            var app = FirebaseApp.Create(new AppOptions
            {
                Credential = credentials
            });
            throw new NotImplementedException();
        }
    }
}
