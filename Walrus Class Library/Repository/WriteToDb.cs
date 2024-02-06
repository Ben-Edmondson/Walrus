using Firebase.Database;
using Firebase.Database.Query;
using Walrus_Class_Library.Models;

namespace Walrus_Class_Library.Repository
{
    public class WriteToDb : IWriteToDatabase
    {

        public async Task WriteData(FirebaseClient firebaseClient)
        {
            try
            {

                var tempDummyData = new UserPresenceViewModel
                {
                    UID = "123456",
                    ConnectionStatus = "Online",
                    LastOnline = DateTime.Now
                };


                await firebaseClient
                    .Child("presence")
                    .Child(tempDummyData.UID)
                    .PutAsync(tempDummyData);

                Console.WriteLine("Data written to Firebase Realtime Database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing data to Firebase Realtime Database: {ex.Message}");
            }
        }
    }
}
