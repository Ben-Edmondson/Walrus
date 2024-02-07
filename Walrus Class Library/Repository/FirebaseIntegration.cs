using Firebase.Database;
using Firebase.Database.Query;
using Walrus_Class_Library.Models;

namespace Walrus_Class_Library.Repository
{
    public class FirebaseIntegration : IFirebaseIntegration
    {

        public async Task WriteData(FirebaseClient firebaseClient, UserPresenceViewModel data)
        {
            try
            {
                await firebaseClient
                    .Child("presence")
                    .Child(data.UID)
                    .PutAsync(data);

                Console.WriteLine("Data written to Firebase Realtime Database successfully.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing data to Firebase Realtime Database: {ex.Message}");
            }

        }

        public async Task UpdateUserToOffline(FirebaseClient firebaseClient, UserPresenceViewModel data)
        {
            try
            {
                data.ConnectionStatus = false;
                data.LastOnline = DateTime.UtcNow;
                await firebaseClient
                    .Child("presence")
                    .Child(data.UID)
                    .PutAsync(data);

                Console.WriteLine($"User {data.UID} is now offline.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting user {data.UID} offline: {ex.Message}");
            }
            Console.ReadLine();
        }
    }

}
