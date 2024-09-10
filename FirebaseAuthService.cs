using System.Threading.Tasks;
using Firebase.Auth;

public class FirebaseAuthService
{
    private readonly FirebaseAuth _auth;

    public FirebaseAuthService()
    {
        _auth = FirebaseAuth.Instance;
    }

    public async Task<string?> SignInWithEmailAndPasswordAsync(string email, string password)
    {
        try
        {
            var user = await _auth.SignInWithEmailAndPasswordAsync(email, password);
            return user.User.Uid; // Return user's Firebase UID
        }
        catch (FirebaseAuthException)
        {
            // Handle sign-in failure
            return null;
        }
    }

    public async Task<string?> CreateUserWithEmailAndPasswordAsync(string email, string password)
    {
        try
        {
            var user = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            return user.User.Uid; // Return user's Firebase UID
        }
        catch (FirebaseAuthException)
        {
            // Handle sign-up failure
            return null;
        }
    }

    public void SignOut()
    {
        _auth.SignOut();
    }
}
