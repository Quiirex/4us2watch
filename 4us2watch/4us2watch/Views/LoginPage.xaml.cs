using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _4us2watch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            auth = DependencyService.Get<IAuth>();
        }

        private async void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            string Token = await auth.LoginWithEmailPassword(Email.Text.Replace(" ", string.Empty), Password.Text); //Cleared the error with .Replace that replaces all white spaces in string
            if (Token != "")
            {
                await DisplayAlert("Authentication successful", "Logged in successfully", "OK");
                Email.Text = string.Empty; //da ni že vpisano, v primeru da gre nazaj
                Password.Text = string.Empty;
                await Navigation.PushAsync(new GenreAssignmentPage()); //spremeni na GenreAssignmentPage, ko vemo, da je prvi login
            }
            else
            {
                Email.Text = string.Empty;
                Password.Text = string.Empty;
                await DisplayAlert("Authentication failed", "E-mail/password is incorrect. Try again!", "OK");
            }
        }
    }
}