using Firebase.Database;
using Walrus_Class_Library.Models;

namespace Walrus_Class_Library.Repository
{
    public interface IWriteToDatabase
    {
        Task WriteData(FirebaseClient firebaseClient);
    }
}
