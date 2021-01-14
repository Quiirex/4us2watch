using _4us2watch.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _4us2watch
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new GridPage("dela"));
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
        void GoogleLogin(object sender, EventArgs e)
        {
            DisplayAlert("dela", "dela", "OK");
        }
        void FacebookLogin(object sender, EventArgs e)
        {
            DisplayAlert("dela", "dela", "OK");
        }
    }
}
