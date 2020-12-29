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
            string Token = await auth.LoginWithEmailPassword(Email.Text, Password.Text);
            if (Token != "")
            {
                //await Navigation.PushAsync(new LoggedPage());
                await DisplayAlert("Authentication Succseeded", "You logged in successfully", "OK");
            }
            else
            {
                ShowError();
            }
        }
        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

    }
}