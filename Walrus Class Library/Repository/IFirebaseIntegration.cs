using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Walrus_Class_Library.Models;

namespace Walrus_Class_Library.Repository
{
    public interface IFirebaseIntegration
    {
        Task WriteData(FirebaseClient firebaseClient, UserPresenceViewModel data);
        Task UpdateUserToOffline(FirebaseClient firebaseClient, UserPresenceViewModel data);
    }
}
