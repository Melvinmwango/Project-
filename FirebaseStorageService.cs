using Firebase.Storage;
using System;
using System.Threading.Tasks;
using Android.Net;
using System.IO;

namespace FileSharingAppAndroid
{
    public class FirebaseStorageService
    {
        private FirebaseStorage _storage;

        public FirebaseStorageService()
        {
            _storage = FirebaseStorage.Instance;
        }

        public async Task UploadFileAsync(Uri fileUri, string userId)
        {
            var storageReference = _storage.GetReferenceFromUrl("gs://your-project-id.appspot.com");
            var userReference = storageReference.Child("users").Child(userId).Child(fileUri.LastPathSegment);

            using (var stream = Application.Context.ContentResolver.OpenInputStream(fileUri))
            {
                await userReference.PutStreamAsync(stream);
                // Handle successful upload
            }
        }

        public async Task DownloadFileAsync(string userId, string fileName, string localPath)
        {
            var storageReference = _storage.GetReferenceFromUrl("gs://your-project-id.appspot.com");
            var userReference = storageReference.Child("users").Child(userId).Child(fileName);

            var localFile = new Java.IO.File(localPath);
            await userReference.GetFileAsync(localFile);
            // Handle successful download
        }
    }
}
