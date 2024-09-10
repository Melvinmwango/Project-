using Firebase.Database;
using System;

namespace FileSharingAppAndroid
{
    public class FirebaseDatabaseService
    {
        private FirebaseDatabase _database;

        public FirebaseDatabaseService()
        {
            _database = FirebaseDatabase.Instance;
        }

        public void WriteData(string userId, string data)
        {
            var userReference = _database.GetReference("users").Child(userId);
            userReference.SetValue(data);
        }

        public void ReadData(string userId, Action<DataSnapshot> onDataRead)
        {
            var userReference = _database.GetReference("users").Child(userId);

            userReference.AddListenerForSingleValueEvent(new ValueEventListener(onDataRead));
        }
    }

    public class ValueEventListener : Java.Lang.Object, IValueEventListener
    {
        private readonly Action<DataSnapshot> _onDataRead;

        public ValueEventListener(Action<DataSnapshot> onDataRead)
        {
            _onDataRead = onDataRead;
        }

        public void OnCancelled(DatabaseError error)
        {
            Console.WriteLine("Database read cancelled: " + error.Message);
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            _onDataRead(snapshot);
        }
    }
}
