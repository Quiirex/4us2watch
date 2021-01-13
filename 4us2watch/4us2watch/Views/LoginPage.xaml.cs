using _4us2watch.Components;
using _4us2watch.Models;
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
        RegistrationPage regis = null;
        MainPage home = null;
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
                User user = await ReaderWriter.GetPerson(Email.Text);
                if(user.movies.Count == 0)
                {
                    await Navigation.PushAsync(new GenreAssignmentPage(user.email));
                }
                else
                {
                    await Navigation.PushAsync(new GridPage(user.email));
                }
                Email.Text = string.Empty; //da ni že vpisano, v primeru da gre nazaj
                Password.Text = string.Empty;               
            }
            else
            {
                Email.Text = string.Empty;
                Password.Text = string.Empty;
                await DisplayAlert("Authentication failed", "E-mail/password is incorrect. Try again!", "OK");
            }
        }
        void RedirectToRegister(object sender, EventArgs e)
        {
            if (regis == null)
            {
                regis = new RegistrationPage();
            }
            App.Current.MainPage = new NavigationPage(regis);
        }
        void RedirectHome(object sender, EventArgs e)
        {
            if (home == null)
            {
                home = new MainPage();
            }
            App.Current.MainPage = new NavigationPage(home);
        }
    }
}