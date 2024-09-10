using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using Android.Net;
using System;
using System.Threading.Tasks;

namespace FileSharingAppAndroid
{
    [Activity(Label = "FileSharingApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private FirebaseAuthService _authService;
        private FirebaseDatabaseService _databaseService;
        private FirebaseStorageService _storageService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Android.Resource.Layout.activity_main);

            var options = new FirebaseOptions.Builder()
                .SetApiKey(FirebaseConfig.ApiKey)
                .SetApplicationId(FirebaseConfig.AppId)
                .SetDatabaseUrl(FirebaseConfig.DatabaseUrl)
                .SetStorageBucket(FirebaseConfig.StorageBucket)
                .Build();

            if (FirebaseApp.Instance == null)
            {
                FirebaseApp.InitializeApp(this, options);
            }

            // Initialize Firebase services
            _authService = new FirebaseAuthService();
            _databaseService = new FirebaseDatabaseService();
            _storageService = new FirebaseStorageService();

            // Example Usage
            SignInUser("user@example.com", "password123");

            // Other initialization code

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        private async void SignInUser(string email, string password)
        {
            var userId = await _authService.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("User signed in: " + userId);
            }
            else
            {
                Console.WriteLine("Sign-in failed");
            }
        }
    }
}
