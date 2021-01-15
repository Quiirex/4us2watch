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
            BtnLogIn.IsEnabled = true;
            activityIndicator.IsRunning = false;
            NavigationPage.SetHasNavigationBar(this, false);
            auth = DependencyService.Get<IAuth>();
        }

        private async void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                BtnLogIn.IsEnabled = false;
                activityIndicator.IsRunning = true;
                string Token = await auth.LoginWithEmailPassword(Email.Text.Replace(" ", string.Empty), Password.Text); //Cleared the error with .Replace that replaces all white spaces in string
                if (Token != "")
                {
                    var user = await ReaderWriter.GetPerson(Email.Text);
                    if (user.movies.Count == 0)
                    {
                        activityIndicator.IsRunning = true;
                        await Navigation.PushAsync(new GenreAssignmentPage(user.email));
                        activityIndicator.IsRunning = false;
                        BtnLogIn.IsEnabled = true;
                    }
                    else
                    {
                        activityIndicator.IsRunning = true;
                        await Navigation.PushAsync(new GridPage(user.email));
                        activityIndicator.IsRunning = false;
                        BtnLogIn.IsEnabled = true;
                    }
                    Email.Text = string.Empty; //da ni že vpisano, v primeru da gre nazaj
                    Password.Text = string.Empty;
                }
                else
                {
                    Email.Text = string.Empty;
                    Password.Text = string.Empty;
                    BtnLogIn.IsEnabled = true;
                    activityIndicator.IsRunning = false;
                    await DisplayAlert("Authentication failed", "E-mail/password is incorrect. Try again!", "OK");
                }
            }
            catch
            {
                BtnLogIn.IsEnabled = true;
                activityIndicator.IsRunning = false;
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
        protected override bool OnBackButtonPressed() => true; //da ne more backoutat, ker se ruši navigacija
    }
}