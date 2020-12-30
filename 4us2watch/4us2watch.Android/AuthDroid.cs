using System;
using System.Threading.Tasks;
using _4us2watch.Droid;
using Firebase.Auth;
using Xamarin.Forms;


[assembly: Dependency(typeof(AuthDroid))]
namespace _4us2watch.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                //e.PrintStackTrace();
                Console.WriteLine(e); //da ne javlja warninga, drugače se itak ne prikaže
                return "";
            }
        }
        public async Task<bool> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                var signUpTask = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return signUpTask != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //da ne javlja warninga, drugače se itak ne prikaže
                return false;
            }
        }

    }
}